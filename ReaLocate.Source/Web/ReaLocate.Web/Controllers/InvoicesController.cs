using Microsoft.AspNet.Identity;
using ReaLocate.Data.Models;
using ReaLocate.Services.Data;
using ReaLocate.Services.Data.Contracts;
using ReaLocate.Web.ViewModels;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Rotativa;
using ReaLocate.Web.Infrastructure.Mapping;

namespace ReaLocate.Web.Controllers
{
    [Authorize]
    public class InvoicesController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IInvoicesService invoicesService;
        private readonly IAgenciesService agencyServices;

        public InvoicesController(IUsersService usersService, IInvoicesService invoicesService, IAgenciesService agencyServices)
        {
            this.usersService = usersService;
            this.invoicesService = invoicesService;
            this.agencyServices = agencyServices;
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
            var dbInvoice = this.invoicesService.GetByEncodedId(id);
            var viewInvoice = this.Mapper.Map<UserInvoiceViewModel>(dbInvoice);
            viewInvoice.EncodedId = id;

            return new ViewAsPdf("InvoiceDetails", viewInvoice) { FileName = "Invoice" + id + ".pdf" };
        }

        [HttpGet]
        public ActionResult PreviewAgencyInvoices(string id)
        {
            var dbAgency = this.agencyServices.GetByEncodedId(id);

            var agencyInvoices = dbAgency.Invoices.AsQueryable()
                .To<AgencyInvoiceViewModel>()
                .OrderByDescending(i => i.CreatedOn)
                .ToList();

            foreach(var item in agencyInvoices)
            {
                item.EncodedId = this.agencyServices.EncodeId(item.Id);
            }

            return this.View(agencyInvoices);
        }

        [HttpGet]
        public ActionResult InvoiceAgencyDetails(string id)
        {
            var dbInvoice = this.invoicesService.GetByEncodedId(id);
            var invoiceToDisplay = this.Mapper.Map<AgencyInvoiceViewModel>(dbInvoice);
            invoiceToDisplay.EncodedId = this.agencyServices.EncodeId(dbInvoice.Id);

            return this.View(invoiceToDisplay);
        }

        public ActionResult PrintAgencyInvoice(string id)
        {
            var dbInvoice = this.invoicesService.GetByEncodedId(id);
            var viewInvoice = this.Mapper.Map<AgencyInvoiceViewModel>(dbInvoice);
            viewInvoice.EncodedId = id;

            return new ViewAsPdf("InvoiceAgencyDetails", viewInvoice) { FileName = "Invoice" + id + ".pdf" };
        }
    }
}