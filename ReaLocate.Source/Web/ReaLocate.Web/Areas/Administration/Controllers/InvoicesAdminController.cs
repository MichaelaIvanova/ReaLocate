namespace ReaLocate.Web.Areas.Administration.Controllers
{
    using System;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using ReaLocate.Data.Models;
    using ReaLocate.Services.Data;
    using ReaLocate.Web.Areas.Administration.ViewModels;
    using ReaLocate.Web.Infrastructure.Mapping;
    using ReaLocate.Web.Controllers;
    using Services.Data.Contracts;

    [Authorize(Roles ="Admin")]
    public class InvoicesAdminController : BaseController
    {
        private readonly IInvoicesService invoiceService;

        public InvoicesAdminController(IInvoicesService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        public ActionResult InvoicesAdmin()
        {
            return View();
        }

        public ActionResult Invoices_Read([DataSourceRequest]DataSourceRequest request)
        {
            var agencies = this.invoiceService.GetAll()
                        .To<InvoiceAdminPanelViewModel>()
                        .ToDataSourceResult(request);

            return Json(agencies);
        }
    }
}