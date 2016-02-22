namespace ReaLocate.Web.Areas.AgencyAdministration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Web.Controllers;

    [Authorize(Roles = "AgencyOwner")]
    [ValidateInput(false)]
    public class MyAgencyAdminController : BaseController
    {
        // GET: AgencyAdministration/MyAgencyAdmin/Menu
        [ChildActionOnly]
        public ActionResult Menu()
        {
            IEnumerable<string> items = new List<string>() { "RealEstateOffers", "MyAgencyInvoices" };
            return this.PartialView("_AdminMenu", items);
        }
    }
}