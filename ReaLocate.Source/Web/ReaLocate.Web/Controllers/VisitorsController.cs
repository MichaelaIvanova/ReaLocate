using Microsoft.AspNet.Identity;
using ReaLocate.Data.Models;
using ReaLocate.Services.Data;
using ReaLocate.Services.Data.Contracts;
using ReaLocate.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReaLocate.Web.Controllers
{
    [Authorize]
    public class VisitorsController : BaseController
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
            this.currentlyLoggedUser = this.usersService.GetUserDetailsById(userId);

            var dbRealEstate = this.realEstatesService.GetByEncodedId(id);
            var visitordDetailsId = (int)dbRealEstate.VisitorsDetailsId;
            var dbvisitors = this.visitorsService.GetById(visitordDetailsId);

            var visitors = dbvisitors.AllUsers;
            var viewVisitors = new List<VisitorViewModel>();

            foreach (var user in visitors)
            {
                var userView = this.Mapper.Map<VisitorViewModel>(user);
                viewVisitors.Add(userView);
            }

            return View(viewVisitors);
        }

        public ActionResult VisitorDetails(string id)
        {
            var dbUser = this.usersService.GetUserDetailsById(id);
            var userView = this.Mapper.Map<VisitorDetailsViewModel>(dbUser);

            return View(userView);
        }
    }
}