namespace ReaLocate.Web.Controllers
{
    using Services.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels;

    [Authorize(Roles = "AgencyOwner")]
    public class InviteBrokersController : Controller
    {
        private readonly IUsersService usersService;

        public InviteBrokersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public ActionResult InviteBrokers()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InviteBrokers(string id, BrokerInputViewModel broker)
        {

            return this.RedirectToAction("AgencyDetails", "Agencies", new { id = id });
        }

        public ActionResult GetAllUsersUserNames()
        {
            //TODO: Cache it
            var users = this.usersService.GetAll()
                .Where(u => u.MyOwnAgencyId == null && u.AgencyWorkForId == null)
                .Select(u => u.UserName)
                .ToList();

            return this.Json(new[] { users });
        }
    }
}