namespace ReaLocate.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

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

            foreach(var estate in estates)
            {
                estate.EncodedId = this.realEstateService.EncodeId(estate.Id);
            }
            
            return this.View(estates);
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
