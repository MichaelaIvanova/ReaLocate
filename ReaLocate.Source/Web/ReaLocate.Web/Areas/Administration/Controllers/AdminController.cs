namespace ReaLocate.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Web.Controllers;

    [Authorize(Roles = "Admin")]
    [ValidateInput(false)]
    public class AdminController : BaseController
    {
        // GET: Administration/Admin/Menu
        [ChildActionOnly]
        public ActionResult Menu()
        {
            IEnumerable<string> items = new List<string>() { "RealEstateAdmin","AgenciesAdmin", "Users" };
            return this.PartialView("_AdminMenu", items);
        }
    }
}