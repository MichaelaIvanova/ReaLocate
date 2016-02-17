namespace ReaLocate.Web.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class VisitorViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePicturePath { get; set; }

    }
}