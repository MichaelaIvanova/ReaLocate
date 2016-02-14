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
    using Web.Infrastructure.Mapping;
    using System.IO;
    using System.Threading.Tasks;
    [Authorize]
    public class RealEstatesController : BaseController
    {
        private readonly IRealEstatesService realEstatesService;
        private readonly IPhotosService photosService;

        public RealEstatesController(IRealEstatesService realEstatesService, IPhotosService photosService)
        {
            this.realEstatesService = realEstatesService;
            this.photosService = photosService;
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
            // TODO get user and see if he has agency
            var userId = this.User.Identity.GetUserId();
            var dbRealEstate = this.Mapper.Map<RealEstate>(realEstate);
            this.realEstatesService.Add(dbRealEstate);
            var realEstateEncodedId = this.realEstatesService.EncodeId(dbRealEstate.Id);

            // TODO: refactor, make method
            foreach (var photo in files)
            {
                // TODO make validation for content lenght
                if (photo != null && photo.ContentLength > 0 && photo.ContentType == "image/jpeg")
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

            return this.RedirectToAction("RealEstateDetails", "RealEstates", new { id = realEstateEncodedId });
        }

        public ActionResult RealEstateDetails(string id)
        {
            var dbRealEstate = this.realEstatesService.GetByEncodedId(id);
            DetailsRealEstateViewModel viewRealEstate = this.Mapper.Map<DetailsRealEstateViewModel>(dbRealEstate);

            return this.View(viewRealEstate);
        }
    }
}