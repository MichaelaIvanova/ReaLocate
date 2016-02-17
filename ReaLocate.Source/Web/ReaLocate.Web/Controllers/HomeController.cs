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
            return this.View();
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
