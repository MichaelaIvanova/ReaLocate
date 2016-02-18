namespace ReaLocate.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using ReaLocate.Data.Models;
    using ReaLocate.Data;
    using ReaLocate.Services.Data;
    using ReaLocate.Web.Areas.Administration.ViewModels;
    using ReaLocate.Web.Infrastructure.Mapping;
    using ReaLocate.Web.ViewModels;
    using ReaLocate.Web.Controllers;

    public class UsersController : BaseController
    {

        private readonly IUsersService usersService;
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Users_Read([DataSourceRequest]DataSourceRequest request)
        {
           var users = this.usersService.GetAll()
                       .To<UserAdminPanelViewModel>()
                       .ToDataSourceResult(request);

            return Json(users);
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Users_Create([DataSourceRequest]DataSourceRequest request, User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var entity = new User
        //        {
        //            FirstName = user.FirstName,
        //            LastName = user.LastName,
        //            ProfilePicturePath = user.ProfilePicturePath,
        //            Email = user.Email,
        //            EmailConfirmed = user.EmailConfirmed,
        //            PasswordHash = user.PasswordHash,
        //            SecurityStamp = user.SecurityStamp,
        //            PhoneNumber = user.PhoneNumber,
        //            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
        //            TwoFactorEnabled = user.TwoFactorEnabled,
        //            LockoutEndDateUtc = user.LockoutEndDateUtc,
        //            LockoutEnabled = user.LockoutEnabled,
        //            AccessFailedCount = user.AccessFailedCount,
        //            UserName = user.UserName
        //        };

        //        db.Users.Add(entity);
        //        db.SaveChanges();
        //        user.Id = entity.Id;
        //    }

        //    return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Update([DataSourceRequest]DataSourceRequest request, UserAdminInputViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.Mapper.Map<User>(user);
                this.usersService.Update(entity);
            }

            var userFromDb = this.usersService.GetUserDetailsById(user.Id);
            var userToDisplay = this.Mapper.Map<UserAdminPanelViewModel>(userFromDb);

            return Json(new[] { userToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Users_Destroy([DataSourceRequest]DataSourceRequest request, User user)
        //{

        //    var entity = new User
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        ProfilePicturePath = user.ProfilePicturePath,
        //        Email = user.Email,
        //        EmailConfirmed = user.EmailConfirmed,
        //        PasswordHash = user.PasswordHash,
        //        SecurityStamp = user.SecurityStamp,
        //        PhoneNumber = user.PhoneNumber,
        //        PhoneNumberConfirmed = user.PhoneNumberConfirmed,
        //        TwoFactorEnabled = user.TwoFactorEnabled,
        //        LockoutEndDateUtc = user.LockoutEndDateUtc,
        //        LockoutEnabled = user.LockoutEnabled,
        //        AccessFailedCount = user.AccessFailedCount,
        //        UserName = user.UserName
        //    };

        //    db.Users.Attach(entity);
        //    db.Users.Remove(entity);
        //    db.SaveChanges();

        //    return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        //{
        //    var fileContents = Convert.FromBase64String(base64);

        //    return File(fileContents, contentType, fileName);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
