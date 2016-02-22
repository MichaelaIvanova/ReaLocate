using ReaLocate.Data.Models;
using ReaLocate.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReaLocate.Web.Areas.Administration.ViewModels
{
    public class AgencyAdminPanelViewModel : IMapFrom<Agency>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UserAdminPanelViewModel Owner { get; set; }

        public int? PackageValue { get; set; }

    }
}