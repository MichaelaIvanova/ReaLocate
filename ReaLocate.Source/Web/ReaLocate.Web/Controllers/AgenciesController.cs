namespace ReaLocate.Web.Controllers
{
    using Data.Models;
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

            if (userId == null || currentlyLoggedUser.MyOwnAgencyId != null)
            {
                // TODO:  redirect ro error page
                return this.RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAgency(CreateAgencyViewModel agency)
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetails(userId);

            var dbAgency = this.Mapper.Map<Agency>(agency);
            dbAgency.OwnerId = userId;
            dbAgency.Owner = currentlyLoggedUser;

            var newAgencyId = this.agenciesService.Add(dbAgency);
            var encodedId = this.agenciesService.EncodeId(newAgencyId);

            return this.RedirectToAction("AgencyDetails", "Agencies", new { id = encodedId });
        }

        public ActionResult AgencyDetails(string id)
        {
            var dbAgency = this.agenciesService.GetByEncodedId(id);

            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetails(userId);

            var viewAgency = this.Mapper.Map<DetailsAgencyViewModel>(dbAgency);

            currentlyLoggedUser.MyOwnAgencyId = dbAgency.Id;
            currentlyLoggedUser.MyOwnAgency = dbAgency;
            this.usersService.Update(currentlyLoggedUser);

            viewAgency.EncodedId = id;
            return this.View(viewAgency);
        }
    }
}