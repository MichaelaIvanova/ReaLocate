namespace ReaLocate.Web.Areas.AgencyAdministration.Controllers
{
    using Administration.ViewModels;
    using Kendo.Mvc.UI;
    using ReaLocate.Services.Data.Contracts;
    using ReaLocate.Web.Controllers;
    using System.Web.Mvc;
    using ReaLocate.Web.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;
    using Services.Data;
    using System.Linq;

    public class MyAgencyInvoicesController : BaseController
    {
        private readonly IInvoicesService invoiceService;
        private readonly IUsersService usersService;

        public MyAgencyInvoicesController(IInvoicesService invoiceService, IUsersService usersService)
        {
            this.invoiceService = invoiceService;
            this.usersService = usersService;
        }

        public ActionResult MyAgencyInvoices()
        {
            return View();
        }

        public ActionResult MyAgencyInvoices_Read([DataSourceRequest]DataSourceRequest request)
        {
            var currentlyLoggedUser = this.usersService.GetUserDetailsById(this.User.Identity.GetUserId());
  
            var invoices = this.invoiceService.GetAll().Where(o=>o.AgencyRecepientId==currentlyLoggedUser.MyOwnAgencyId)
                        .To<InvoiceAdminPanelViewModel>()
                        .ToDataSourceResult(request);

            return Json(invoices);
        }
    }
}