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
    using Helpers;
    [Authorize]
    public class RealEstatesController : BaseController
    {
        protected readonly IRealEstatesService realEstatesService;
        protected readonly IPhotosService photosService;
        protected readonly IVisitorsService visitorsService;
        protected readonly IUsersService usersService;
        protected readonly IUsersRolesService rolesService;
        protected readonly IRealEstateCreateUtil util;

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
        public virtual ActionResult CreateRealEstate()
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(userId);
            var roleCount = currentlyLoggedUser.Roles.Count();
            this.ViewBag.MaxPhotos = 3;

            if (roleCount != 0)
            {
                var roleId = currentlyLoggedUser.Roles.First().RoleId;
                var roleType = this.rolesService.GetRoleById(roleId).Name;

                if (roleType == "AgencyOwner")
                {
                    return this.RedirectToAction("CreateRealEstate", "AgencyRealEstates");
                }
                else if (roleType == "Broker")
                {
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
        public virtual async Task<ActionResult> CreateRealEstate(CreateRealEstateViewModel realEstateInput, IEnumerable<HttpPostedFileBase> files)
        {
            var addressFull = this.util.GetRealAddress(realEstateInput);
            var address = @addressFull.FormattedAddress;

            if (!this.ModelState.IsValid)
            {
                return this.View(realEstateInput);
            }

            if (address != null)
            {
                RealEstate dbRealEstate = CreateRealEstate(realEstateInput, addressFull);
                this.realEstatesService.Add(dbRealEstate);

                var realEstateEncodedId = this.realEstatesService.EncodeId(dbRealEstate.Id);

                foreach (var photo in files)
                {

                    this.SavePhoto(dbRealEstate, realEstateEncodedId, photo);
                }

                if((int)realEstateInput.OfferType == 1 && dbRealEstate.Publisher.MyOwnAgencyId == null) 
                {
                    return this.RedirectToAction("CreateInvoiceRegularUser", "Invoices", new { id = realEstateEncodedId });
                }
                else if((int)realEstateInput.OfferType == 1 && dbRealEstate.Publisher.MyOwnAgencyId !=null)
                {
                    var agencyEncodedId = this.realEstatesService.EncodeId((int)dbRealEstate.Publisher.MyOwnAgencyId);
                    return this.RedirectToAction("PreviewAgencyInvoices", "Invoices", new { id = agencyEncodedId});
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
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(userId);

            //update entity
            dbVisitors.AllUsers.Add(currentlyLoggedUser);
            this.visitorsService.Update(dbVisitors);

            DetailsRealEstateViewModel viewRealEstate = this.Mapper.Map<DetailsRealEstateViewModel>(dbRealEstate);
            viewRealEstate.EncodedId = id;
            return this.View(viewRealEstate);
        }

        public ActionResult RealEstateDetailsByIntId(string id)
        {
            var encodedId = this.realEstatesService.EncodeId(int.Parse(id));
            return this.RedirectToAction("RealEstateDetails", "RealEstates", new { id = encodedId });
        }

        private RealEstate CreateRealEstate(CreateRealEstateViewModel realEstate, GoogleAddress addressFull)
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(userId);

            realEstate.Country = addressFull.Components[3].LongName;
            realEstate.City = addressFull.Components[1].LongName;

            var dbRealEstate = this.Mapper.Map<RealEstate>(realEstate);
            dbRealEstate.PublisherId = userId;
            if (currentlyLoggedUser.MyOwnAgencyId !=null)
            {
                dbRealEstate.AgencyId = currentlyLoggedUser.MyOwnAgencyId;
            }

            dbRealEstate.Latitude = addressFull.Coordinates.Latitude;
            dbRealEstate.Longitude = addressFull.Coordinates.Longitude;

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