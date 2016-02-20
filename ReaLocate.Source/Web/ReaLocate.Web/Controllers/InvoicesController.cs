using Microsoft.AspNet.Identity;
using ReaLocate.Data.Models;
using ReaLocate.Services.Data;
using ReaLocate.Services.Data.Contracts;
using ReaLocate.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace ReaLocate.Web.Controllers
{
    [Authorize]
    public class InvoicesController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IInvoicesService invoicesService;

        public InvoicesController(IUsersService usersService, IInvoicesService invoicesService)
        {
            this.usersService = usersService;
            this.invoicesService = invoicesService;
        }

        [HttpGet]
        public ActionResult CreateInvoiceRegularUser(string id)
        {
            this.ViewBag.CurrentEstateId = id;
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInvoiceRegularUser(string id, InvoiceByUserViewModel invoiceDetails)
        {
            var userId = this.User.Identity.GetUserId();
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(userId);
            invoiceDetails.RealEstateId = id;


            var dbInvoice = this.Mapper.Map<Invoice>(invoiceDetails);
            dbInvoice.UserRecepientId = userId;
            dbInvoice.UserRecepient = currentlyLoggedUser;
            dbInvoice.Quality = 1;
            // TODO: set total cost as enum
            dbInvoice.TotalCost = 10;

            var idRaw = this.invoicesService.Add(dbInvoice);
            var idEncoded = this.invoicesService.EncodeId(idRaw);

            return this.RedirectToAction("InvoiceDetails", "Invoices", new { id = idEncoded });
            //return this.RedirectToAction("RealEstateDetails", "RealEstates", new { id = id });
        }

        [HttpGet]
        public ActionResult InvoiceDetails(string id)
        {
            var dbInvoice = this.invoicesService.GetByEncodedId(id);
            var viewInvoice = this.Mapper.Map<UserInvoiceViewModel>(dbInvoice);
            viewInvoice.EncodedId = id;
            return View(viewInvoice);
        }

        public ActionResult PrintInvoice(string id)
        {
            return new ActionAsPdf( "InvoiceDetails", new { id = id })
            {
                FileName = "Invoice.pdf"
            };
        }
    }
}