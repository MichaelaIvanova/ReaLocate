namespace ReaLocate.Web.Areas.AgencyAdministration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using ReaLocate.Data.Models;
    using Web.Controllers;
    using Services.Data.Contracts;
    using ReaLocate.Web.Areas.Administration.ViewModels;
    using ReaLocate.Web.Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Services.Data;
    public class RealEstatesOffersController : BaseController
    {
        private readonly IRealEstatesService realEstatesService;
        private readonly IUsersService usersService;

        public RealEstatesOffersController(IRealEstatesService realEstatesService, IUsersService usersService)
        {
            this.realEstatesService = realEstatesService;
            this.usersService = usersService;
        }

        public ActionResult RealEstatesOffers()
        {
            return View();
        }

        public ActionResult RealEstatesOffers_Read([DataSourceRequest]DataSourceRequest request)
        {
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(this.User.Identity.GetUserId());
            var realestates = this.realEstatesService.GetAll()
                .Where(r=>r.AgencyId == currentlyLoggedUser.MyOwnAgencyId)
                .To<AdminRealEstateViewModel>()
                .ToDataSourceResult(request);

            return Json(realestates);
        }
    }
}