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
    using System.Web;
    using System.Web.Mvc;
    using ViewModels;
    public class AgenciesController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IAgenciesService agenciesService;
        private readonly IPaymentDetailsService paymentService;

        public AgenciesController(IUsersService usersService, IAgenciesService agenciesService, IPaymentDetailsService paymentService)
        {
            this.usersService = usersService;
            this.agenciesService = agenciesService;
            this.paymentService = paymentService;
        }

        [HttpGet]
        public ActionResult CreateAgency()
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetails(userId);

            if (this.User.IsInRole("Admin"))
            {
                // TODO:  redirect ro error page - you hava gency alredy
                return this.RedirectToAction("Index", "Home");
            }
            else if (this.User.IsInRole("AgencyOwner"))
            {
                return this.RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAgency(CreateAgencyViewModel agency)
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetails(userId);

            var dbPaymentDetails = this.Mapper.Map<PaymentDetails>(agency.PaymentDetails);
            //save payment details
            this.paymentService.Add(dbPaymentDetails);

            var dbAgency = this.Mapper.Map<Agency>(agency);
            dbAgency.OwnerId = userId;
            dbAgency.Owner = currentlyLoggedUser;

            var newAgencyId = this.agenciesService.Add(dbAgency);
            var encodedId = this.agenciesService.EncodeId(newAgencyId);

            currentlyLoggedUser.MyOwnAgencyId = dbAgency.Id;
            currentlyLoggedUser.MyOwnAgency = dbAgency;
            
            // admin
            currentlyLoggedUser.Roles.Add(new IdentityUserRole() {RoleId= "b83ce087-3ed9-4b5c-8853-d533a8d7bf6e" });
            this.usersService.Update(currentlyLoggedUser);
             
            return this.RedirectToAction("AgencyDetails", "Agencies", new { id = encodedId });
        }

        public ActionResult AgencyDetails(string id)
        {
            var dbAgency = this.agenciesService.GetByEncodedId(id);

            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetails(userId);

            var viewAgency = this.Mapper.Map<DetailsAgencyViewModel>(dbAgency);

            

            viewAgency.EncodedId = id;
            return this.View(viewAgency);
        }
    }
}