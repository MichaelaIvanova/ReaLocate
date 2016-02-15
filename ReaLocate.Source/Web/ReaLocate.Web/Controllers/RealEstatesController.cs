namespace ReaLocate.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels;
    using System.IO;
    using System.Threading.Tasks;
    using Geocoding.Google;
    using Services.Data;

    [Authorize]
    public class RealEstatesController : BaseController
    {
        private readonly IRealEstatesService realEstatesService;
        private readonly IPhotosService photosService;
        private readonly IVisitorsService visitorsService;
        private readonly IUsersService usersService;

        public RealEstatesController(IRealEstatesService realEstatesService, IPhotosService photosService, IVisitorsService visitorsService, IUsersService usersService)
        {
            this.realEstatesService = realEstatesService;
            this.photosService = photosService;
            this.visitorsService = visitorsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public ActionResult CreateRealEstate()
        {
            // TODO, see if user is ana agency or normal
            this.ViewBag.MaxPhotos = 3;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRealEstate(CreateRealEstateViewModel realEstate, IEnumerable<HttpPostedFileBase> files)
        {
            var addressFull = GetRealAddress(realEstate);
            var address = @addressFull.FormattedAddress;

            if (address != null)
            {
                // TODO get user and see if he has agency
                var userId = this.User.Identity.GetUserId();
                var currentUser = this.usersService.GetUserDetails(userId);

                realEstate.Country = addressFull.Components[3].LongName;
                realEstate.City = addressFull.Components[0].LongName;
                var dbRealEstate = this.Mapper.Map<RealEstate>(realEstate);

                var visitors = new VisitorsDetails();
                visitors.AllUsers.Add(currentUser);
                var visitorsDetailsId = this.visitorsService.Add(visitors);
                dbRealEstate.VisitorsDetailsId = visitorsDetailsId;

                this.realEstatesService.Add(dbRealEstate);
                var realEstateEncodedId = this.realEstatesService.EncodeId(dbRealEstate.Id);

                foreach (var photo in files)
                {

                    this.SavePhoto(dbRealEstate, realEstateEncodedId, photo);
                }

                return this.RedirectToAction("RealEstateDetails", "RealEstates", new { id = realEstateEncodedId });
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }

        }

        public ActionResult RealEstateDetails(string id)
        {
            var dbRealEstate = this.realEstatesService.GetByEncodedId(id);

            //this.visitorsService.Add(new VisitorsDetails());

            
            DetailsRealEstateViewModel viewRealEstate = this.Mapper.Map<DetailsRealEstateViewModel>(dbRealEstate);

            return this.View(viewRealEstate);
        }

        private GoogleAddress GetRealAddress(CreateRealEstateViewModel realEstate)
        {
            var geocoder = new GoogleGeocoder();
            if (realEstate.Address != null && realEstate.Address.Length > 5)
            {
                List<GoogleAddress> addresses = geocoder.Geocode(realEstate.Address).ToList();
                var fullAddress = addresses[0];
                return fullAddress;
            }
            return null;
        }

        private void SavePhoto(RealEstate dbRealEstate, string realEstateEncodedId, HttpPostedFileBase photo)
        {
            //// TODO make validation for content lenght - DONE
            if (photo != null && photo.ContentLength > 0 && photo.ContentLength < (1 * 1024 * 1024) && photo.ContentType == "image/jpeg")
            {
                string directory = this.Server.MapPath("~/UploadedFiles/RealEstatePhotos/") + realEstateEncodedId;

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string filename = Guid.NewGuid().ToString() + ".jpg";
                string path = directory + "/" + filename;
                string url = "~/UploadedFiles/RealEstatePhotos/" + realEstateEncodedId + "/" + filename;
                photo.SaveAs(path);
                var newPhoto = new Photo
                {
                    SourceUrl = url,
                    RealEstate = dbRealEstate
                };

                this.photosService.Add(newPhoto);
            }
        }


    }
}