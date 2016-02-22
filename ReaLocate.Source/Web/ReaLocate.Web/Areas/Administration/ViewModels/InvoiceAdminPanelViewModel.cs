namespace ReaLocate.Web.Areas.Administration.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class InvoiceAdminPanelViewModel : IMapFrom<Invoice>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserAdminPanelViewModel UserRecepient { get; set; }

        public AgencyAdminPanelViewModel AgencyRecepient { get; set; }

        public string About { get; set; }

        public string Description { get; set; }

        public int Quality { get; set; }

        public decimal TotalCost { get; set; }
    }
}