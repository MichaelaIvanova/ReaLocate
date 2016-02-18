namespace ReaLocate.Web.Areas.Administration.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;
    using System;
    using System.Linq;

    public class UserAdminInputViewModel : IMapTo<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

    }
}