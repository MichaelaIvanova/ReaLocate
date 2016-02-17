namespace ReaLocate.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using ViewModels;

    public class HomeController : BaseController
    {

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
    }
}
