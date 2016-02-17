namespace ReaLocate.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using Infrastructure.Mapping;
    using ViewModels;
    using Services.Data.Contracts;
    public class HomeController : BaseController
    {
        private readonly IRealEstatesService realEstateService;

        public HomeController(IRealEstatesService realEstateService)
        {
            this.realEstateService = realEstateService;
            
        }

        public ActionResult Index()
        {
            var estates = this.realEstateService.GetAll().To<DetailsRealEstateViewModel>().ToList();
            var coordinates = new List<CoordinateViewModel>();

            foreach(var estate in estates)
            {
                estate.EncodedId = this.realEstateService.EncodeId(estate.Id);
                var coordinate = new CoordinateViewModel
                {
                    Address = estate.Address,
                    GeoLat = estate.Latitude,
                    GeoLong = estate.Longitude
                };
                coordinates.Add(coordinate);
            }

            var indexView = new IndexMapAndGridViewModel
            {
                MapsCoordinates = coordinates,
                Estates = estates
            };
            return this.View(indexView);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CreateRealEstateViewModel realEstate)
        {
            return this.View();
        }

        public ActionResult Chat()
        {
            return View();
        }
    }
}
