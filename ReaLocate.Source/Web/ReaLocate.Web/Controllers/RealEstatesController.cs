namespace ReaLocate.Web.Controllers
{
    using Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels;
    [Authorize]
    public class RealEstatesController : BaseController
    {
        private readonly IRealEstatesService realEstatesService;

        public RealEstatesController(IRealEstatesService realEstatesService)
        {
            this.realEstatesService = realEstatesService;
        }

        [HttpGet]
        public ActionResult CreateRealEstate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRealEstate(RealEstateViewModel realEstate)
        {
            return this.View();
        }
    }
}