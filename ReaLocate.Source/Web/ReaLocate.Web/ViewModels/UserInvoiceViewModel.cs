namespace ReaLocate.Web.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;
    using ReaLocate.Web.ViewModels;
    using System;
    public class UserInvoiceViewModel : IMapFrom<Invoice>
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EncodedId { get; set; }
        public UserAsInvoiceRecepientViewModel UserRecepient { get; set; }

        public string About { get; set; }

        public string Description { get; set; }

        public int Quality { get; set; }

        public decimal TotalCost { get; set; }
    }
}