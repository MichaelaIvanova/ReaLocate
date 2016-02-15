using Microsoft.AspNet.Identity;
using ReaLocate.Data.Models;
using ReaLocate.Services.Data;
using ReaLocate.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReaLocate.Web.Controllers
{
    public class VisitorsController : Controller
    {

        private readonly IUsersService usersService;
        private readonly IVisitorsService visitorsService;
        private readonly IRealEstatesService realEstatesService;
        private User currentlyLoggedUser;

        public VisitorsController(IUsersService usersService, IVisitorsService visitorsService, IRealEstatesService realEstatesService)
        {
            this.usersService = usersService;
            this.visitorsService = visitorsService;
            this.realEstatesService = realEstatesService;
        }

        public ActionResult WhoViewRealEstateDetails(string id)
        {
            //check if user is the owner of the add
            var userId = this.User.Identity.GetUserId();
            this.currentlyLoggedUser = this.usersService.GetUserDetails(userId);

            var dbRealEstate = this.realEstatesService.GetByEncodedId(id);
            var visitordDetailsId = (int)dbRealEstate.VisitorsDetailsId;
            var dbvisitors = this.visitorsService.GetById(visitordDetailsId);

            var visitors = dbvisitors.AllUsers;

            return View(visitors);
        }
    }
}