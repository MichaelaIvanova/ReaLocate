namespace ReaLocate.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Services.Data;
    using Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels;
    using ReaLocate.Web.Infrastructure.Mapping;
    using System.Linq;

    [Authorize(Roles = "AgencyOwner")]
    public class InviteBrokersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IAgenciesService agenciesService;
        private readonly IUsersRolesService rolesService;

        public InviteBrokersController(IUsersService usersService, IAgenciesService agenciesService,
             IUsersRolesService rolesService)
        {
            this.usersService = usersService;
            this.agenciesService = agenciesService;
            this.rolesService = rolesService;
        }

        [HttpGet]
        public ActionResult InviteBrokers()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InviteBrokers(BrokerInputViewModel broker)
        {
            var brokerUser = this.usersService.GetAll()
                .Where(u => u.UserName.Contains(broker.UserName))
                .FirstOrDefault();

            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(userId);
            var agencyEncodedId = this.agenciesService.EncodeId((int)currentlyLoggedUser.MyOwnAgencyId);

            if (brokerUser.MyOwnAgencyId == null && brokerUser.AgencyWorkForId == null)
            {
                var agency = this.agenciesService.GetById((int)currentlyLoggedUser.MyOwnAgencyId);
                brokerUser.AgencyWorkForId = agency.Id;
                brokerUser.AgencyWorkFor = agency;

                var role = this.rolesService.GetRoleByName("Broker");
                brokerUser.Roles.Add(new IdentityUserRole() { RoleId = role.Id });

                agency.Brokers.Add(brokerUser);
                this.agenciesService.Update(agency);
                this.usersService.Update(brokerUser);
            }
            else
            {
                return this.RedirectToAction("ErrorUserAlredyIsInAgency", "Error");
            }


            return this.RedirectToAction("AgencyDetails", "Agencies", new { id = agencyEncodedId });
        }

        public ActionResult GetAllUsersUserNames()
        {
            //TODO: Cache it
            var users = this.usersService.GetAll()
                .Where(u => u.MyOwnAgencyId == null && u.AgencyWorkForId == null)
                .Select(u => u.UserName)
                .ToList();

            return this.Json(new[] { users });
        }

        public ActionResult GetAllBrokersByAgency(string id)
        {
            var dbagency = this.agenciesService.GetByEncodedId(id);
            var agencyBrokers = this.usersService.GetAll()
                .Where(u => u.AgencyWorkForId == dbagency.Id)
                .To<BrokerViewModel>().ToList();

            return this.View(agencyBrokers);
        }
    }
}