namespace ReaLocate.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels;
    using Web.Infrastructure.Mapping;

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
            this.ViewBag.MaxPhotos = 3;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRealEstate(RealEstateViewModel realEstate, IEnumerable<HttpPostedFileBase> files)
        {
            // TODO get user and see if he has agency
            var userId = this.User.Identity.GetUserId();
            var dbRealEstate = this.Mapper.Map<RealEstate>(realEstate);

            this.realEstatesService.Add(dbRealEstate);

            var idCreatedRealEstate = dbRealEstate.Id;
            var encodedId = this.realEstatesService.EncodeId(idCreatedRealEstate);

            return this.RedirectToAction("RealEstateDetails", "RealEstates", new { id = encodedId });
        }

        public ActionResult RealEstateDetails(string id)
        {
            var dbRealEstate = this.realEstatesService.GetByEncodedId(id);
            RealEstateViewModel viewRealEstate = this.Mapper.Map<RealEstateViewModel>(dbRealEstate);

            return this.View(viewRealEstate);
        }
    }
}