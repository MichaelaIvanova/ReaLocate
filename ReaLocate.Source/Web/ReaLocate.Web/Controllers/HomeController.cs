namespace ReaLocate.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mapping;

    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            return this.View();
        }
    }
}
