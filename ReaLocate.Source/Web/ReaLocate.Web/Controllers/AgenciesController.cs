namespace ReaLocate.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using Services.Data;
    using Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels;
    public class AgenciesController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IAgenciesService agenciesService;

        public AgenciesController(IUsersService usersService, IAgenciesService agenciesService)
        {
            this.usersService = usersService;
            this.agenciesService = agenciesService;
        }

        [HttpGet]
        public ActionResult CreateAgency()
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetails(userId);

            if(currentlyLoggedUser.MyOwnAgencyId != null)
            {
                // TODO:  redirect ro error page
                return this.RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAgency( CreateAgencyViewModel agency)
        {
            var userId = this.User.Identity.GetUserId();

            return View();
        }
    }
}