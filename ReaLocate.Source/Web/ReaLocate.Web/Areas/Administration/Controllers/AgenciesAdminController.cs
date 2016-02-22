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
    public class AgenciesAdminController : BaseController
    {
        private readonly IAgenciesService agenciesService;

        public AgenciesAdminController(IAgenciesService agenciesService)
        {
            this.agenciesService = agenciesService;
        }

        public ActionResult AgenciesAdmin()
        {
            return View();
        }

        public ActionResult Agencies_Read([DataSourceRequest]DataSourceRequest request)
        {
            var agencies = this.agenciesService.GetAll()
                        .To<AgencyAdminPanelViewModel>()
                        .ToDataSourceResult(request);

            return Json(agencies);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Agencies_Destroy([DataSourceRequest]DataSourceRequest request, Agency agency)
        {
            var agencyFromDb = this.agenciesService.GetById(agency.Id);
            this.agenciesService.Delete(agencyFromDb);

            return Json(new[] { agency }.ToDataSourceResult(request, this.ModelState));
        }
    }
}