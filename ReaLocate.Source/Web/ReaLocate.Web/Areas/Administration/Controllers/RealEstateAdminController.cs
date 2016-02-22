namespace ReaLocate.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using ReaLocate.Data.Models;
    using Web.Controllers;
    using Services.Data.Contracts;
    using ReaLocate.Web.Areas.Administration.ViewModels;
    using ReaLocate.Web.Infrastructure.Mapping;


    public class RealEstateAdminController : BaseController
    {
        private readonly IRealEstatesService realEstatesService;

        public RealEstateAdminController(IRealEstatesService realEstatesService)
        {
            this.realEstatesService = realEstatesService;
        }

        public ActionResult RealEstateAdmin()
        {
            return View();
        }

        public ActionResult RealEstates_Read([DataSourceRequest]DataSourceRequest request)
        {
            var realestates = this.realEstatesService.GetAll()
                .To<AdminRealEstateViewModel>()
                .ToDataSourceResult(request);

            return Json(realestates);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RealEstates_Update([DataSourceRequest]DataSourceRequest request, AdminRealEstateViewModel realEstate)
        {
            if (ModelState.IsValid)
            {
                var entity = this.Mapper.Map<RealEstate>(realEstate);
                this.realEstatesService.Update(entity);

            }
            var realEstateFromDb = this.realEstatesService.GetById(realEstate.Id).First();
            var realestateToDisplay = this.Mapper.Map<AdminRealEstateViewModel>(realEstateFromDb);

            return Json(new[] { realestateToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RealEstates_Destroy([DataSourceRequest]DataSourceRequest request, RealEstate realEstate)
        {
            var realEstateFromDb = this.realEstatesService.GetById(realEstate.Id).First();
            this.realEstatesService.Delete(realEstateFromDb);

            return Json(new[] { realEstate }.ToDataSourceResult(request, this.ModelState));

        }
    }
}
