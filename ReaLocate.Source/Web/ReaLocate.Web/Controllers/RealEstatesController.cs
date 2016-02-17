namespace ReaLocate.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels;
    using System.Threading.Tasks;
    using Geocoding.Google;
    using Services.Data;
    using System.IO;
    using System;
    [Authorize]
    public class RealEstatesController : BaseController
    {
        private readonly IRealEstatesService realEstatesService;
        private readonly IPhotosService photosService;
        private readonly IVisitorsService visitorsService;
        private readonly IUsersService usersService;
        private readonly IUsersRolesService rolesService;
        private readonly IRealEstateCreateUtil util;

        public RealEstatesController(IRealEstatesService realEstatesService, IPhotosService photosService, IVisitorsService visitorsService,
            IUsersService usersService, IUsersRolesService rolesService, IRealEstateCreateUtil util)
        {
            this.realEstatesService = realEstatesService;
            this.photosService = photosService;
            this.visitorsService = visitorsService;
            this.usersService = usersService;
            this.rolesService = rolesService;
            this.util = util;
        }

        [HttpGet]
        public ActionResult CreateRealEstate()
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetails(userId);
            var roleCount = currentlyLoggedUser.Roles.Count();
            this.ViewBag.MaxPhotos = 3;

            if (roleCount != 0)
            {
                var roleId = currentlyLoggedUser.Roles.First().RoleId;
                var roleType = this.rolesService.GetRoleById(roleId).Name;

                if (roleType == "AgencyOwner")
                {
                    this.ViewBag.MaxPhotos = 5;
                    return this.RedirectToAction("ErrorAgency", "Error");
                }
                else if (roleType == "Broker")
                {
                    this.ViewBag.MaxPhotos = 5;
                    return this.RedirectToAction("ErrorBroker", "Error");
                }
                else if (roleType == "Admin")
                {
                    return this.RedirectToAction("ErrorAdmin", "Error");
                }

                return this.View();
            }

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRealEstate(CreateRealEstateViewModel realEstate, IEnumerable<HttpPostedFileBase> files)
        {
            var addressFull = this.util.GetRealAddress(realEstate);
            var address = @addressFull.FormattedAddress;

            if (address != null)
            {
                RealEstate dbRealEstate = CreateRealEstate(realEstate, addressFull);
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
            var visitorsId = (int)dbRealEstate.VisitorsDetailsId;
            VisitorsDetails dbVisitors = this.visitorsService.GetById(visitorsId);

            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetails(userId);

            //update entity
            dbVisitors.AllUsers.Add(currentlyLoggedUser);
            this.visitorsService.Update(dbVisitors);

            DetailsRealEstateViewModel viewRealEstate = this.Mapper.Map<DetailsRealEstateViewModel>(dbRealEstate);
            viewRealEstate.EncodedId = id;
            return this.View(viewRealEstate);
        }

        private RealEstate CreateRealEstate(CreateRealEstateViewModel realEstate, GoogleAddress addressFull)
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetails(userId);

            realEstate.Country = addressFull.Components[3].LongName;
            realEstate.City = addressFull.Components[0].LongName;
            var dbRealEstate = this.Mapper.Map<RealEstate>(realEstate);

            var visitors = new VisitorsDetails();
            visitors.AllUsers.Add(currentlyLoggedUser);

            var visitorsDetailsId = this.visitorsService.Add(visitors);
            dbRealEstate.VisitorsDetailsId = visitorsDetailsId;
            dbRealEstate.VisitorsDetails = visitors;
            return dbRealEstate;
        }

        public void SavePhoto(RealEstate dbRealEstate, string realEstateEncodedId, HttpPostedFileBase photo)
        {
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