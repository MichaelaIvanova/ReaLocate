﻿namespace ReaLocate.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Services.Data;
    using Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using ViewModels;

    [Authorize]
    public class AgenciesController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IAgenciesService agenciesService;
        private readonly IPaymentDetailsService paymentService;
        private readonly IUsersRolesService rolesService;
        private readonly IInvoicesService invoicesService;

        public AgenciesController(IUsersService usersService, IAgenciesService agenciesService,
            IPaymentDetailsService paymentService, IUsersRolesService rolesService, IInvoicesService invoicesService)
        {
            this.usersService = usersService;
            this.agenciesService = agenciesService;
            this.paymentService = paymentService;
            this.rolesService = rolesService;
            this.invoicesService = invoicesService;
        }

        public AgenciesController()
        {
        }

        [HttpGet]
        public ActionResult CreateAgency()
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(userId);
            var roleCount = currentlyLoggedUser.Roles.Count();


            if (roleCount != 0)
            {
                var roleId = currentlyLoggedUser.Roles.First().RoleId;
                var roleType = this.rolesService.GetRoleById(roleId).Name;

                if (roleType == "AgencyOwner")
                {
                    return this.RedirectToAction("ErrorAgency", "Error");
                }
                else if (roleType == "Admin")
                {
                    return this.RedirectToAction("ErrorAdmin", "Error");
                }
                else if (roleType == "Broker")
                {
                    return this.RedirectToAction("ErrorBroker", "Error");
                }

                return View();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAgency(CreateAgencyViewModel agency)
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(userId);

            var dbPaymentDetails = this.Mapper.Map<PaymentDetails>(agency.PaymentDetails);
            this.paymentService.Add(dbPaymentDetails);
            var dbAgency = this.Mapper.Map<Agency>(agency);
            dbAgency.OwnerId = userId;
            dbAgency.Owner = currentlyLoggedUser;

            if (agency.HasPackage)
            {
                dbAgency.PackageValue = 10;
            }

            var newAgencyId = this.agenciesService.Add(dbAgency);
            currentlyLoggedUser.MyOwnAgencyId = dbAgency.Id;
            currentlyLoggedUser.MyOwnAgency = dbAgency;
            var role = this.rolesService.GetRoleByName("AgencyOwner");
            currentlyLoggedUser.Roles.Add(new IdentityUserRole() { RoleId = role.Id });
            this.usersService.Update(currentlyLoggedUser);

            if (agency.HasPackage)
            {

                var dbInvoice = new Invoice
                {
                    Quality = 10,
                    Description ="My agency invoice for package",
                    About="About real Estate",
                    TotalCost=20,
                    UserRecepientId=currentlyLoggedUser.Id,
                    UserRecepient=currentlyLoggedUser,
                    AgencyRecepient=this.agenciesService.GetById(newAgencyId),
                };
                this.invoicesService.Add(dbInvoice);
            }

            var encodedId = this.agenciesService.EncodeId(newAgencyId);

            return this.RedirectToAction("AgencyDetails", "Agencies", new { id = encodedId });
        }

        public ActionResult GetMyAgency()
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(userId);

            var dbAgency = this.agenciesService.GetById((int)currentlyLoggedUser.MyOwnAgencyId);
            var encodedId = this.agenciesService.EncodeId(dbAgency.Id);

            return this.RedirectToAction("AgencyDetails", "Agencies", new { id = encodedId });
        }

        public ActionResult AgencyDetails(string id)
        {
            var dbAgency = this.agenciesService.GetByEncodedId(id);

            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(userId);

            var viewAgency = this.Mapper.Map<DetailsAgencyViewModel>(dbAgency);
            viewAgency.EncodedId = id;
            return this.View(viewAgency);
        }
    }
}