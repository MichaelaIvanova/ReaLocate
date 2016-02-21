namespace ReaLocate.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Services.Data;
    using Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using ViewModels;

    public class AgencyRealEstatesController : RealEstatesController
    {

        public AgencyRealEstatesController(IRealEstatesService realEstatesService, IPhotosService photosService, IVisitorsService visitorsService,
            IUsersService usersService, IUsersRolesService rolesService, IRealEstateCreateUtil util)
            : base(realEstatesService, photosService, visitorsService, usersService, rolesService, util)
        {
        }

        [HttpGet]
        public override ActionResult CreateRealEstate()
        {
            this.ViewBag.MaxPhotos = 5;
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<ActionResult> CreateRealEstate(CreateRealEstateViewModel realEstateInput, IEnumerable<HttpPostedFileBase> files)
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(userId);

            //is offer gold and any offers left
            if ((int)realEstateInput.OfferType == 1 && currentlyLoggedUser.MyOwnAgency.PackageValue >0)
            {
                currentlyLoggedUser.MyOwnAgency.PackageValue -= 1;
                this.usersService.Update(currentlyLoggedUser);
            }

            return await base.CreateRealEstate(realEstateInput, files);
        }

    }
}