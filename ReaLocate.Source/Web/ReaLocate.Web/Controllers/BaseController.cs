namespace ReaLocate.Web.Controllers
{
    using System.Web.Mvc;

    using ReaLocate.Services.Web;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }
    }
}
