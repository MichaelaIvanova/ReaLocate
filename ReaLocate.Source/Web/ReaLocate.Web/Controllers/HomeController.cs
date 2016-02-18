namespace ReaLocate.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using Infrastructure.Mapping;
    using ViewModels;
    using Services.Data.Contracts;
    using System;
    public class HomeController : BaseController
    {
        private const int ItemsPerPage = 10;
        private readonly IRealEstatesService realEstateService;

        public HomeController(IRealEstatesService realEstateService)
        {
            this.realEstateService = realEstateService;
        }

        public ActionResult Index(string id)
        {
            int page;
            if (id == string.Empty || id == null)
            {
                page = 1;
            }
            else
            {
                page = int.Parse(id);
            }

            var allItemsCount = this.realEstateService.GetAll().Count();
            var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;

            var estates =
               this.Cache.Get(
                   "realEstatePerPage",
                   () => this.realEstateService.GetAllForPaging(itemsToSkip, ItemsPerPage)
                         .To<DetailsRealEstateViewModel>().ToList(),
                   15 * 60);
            //var estates = this.realEstateService.GetAllForPaging(itemsToSkip, ItemsPerPage)
            //             .To<DetailsRealEstateViewModel>().ToList();
            //List<CoordinateViewModel> coordinates = GetCoordinates(estates);
            var coordinates =
               this.Cache.Get(
                   "coordinatesPerPage",
                   () => GetCoordinates(estates),
                   15 * 60);

            var indexView = new IndexMapAndGridViewModel
            {
                MapsCoordinates = coordinates,
                Estates = estates,
                TotalPages = totalPages,
                CurrentPage = page
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

        private List<CoordinateViewModel> GetCoordinates(List<DetailsRealEstateViewModel> estates)
        {
            var coordinates = new List<CoordinateViewModel>();

            foreach (var estate in estates)
            {
                estate.EncodedId = this.realEstateService.EncodeId(estate.Id);
                var coordinate = new CoordinateViewModel
                {
                    Address = estate.Address,
                    EncodedId = estate.EncodedId,
                    GeoLat = estate.Latitude,
                    GeoLong = estate.Longitude
                };
                coordinates.Add(coordinate);
            }

            return coordinates;
        }
    }
}
