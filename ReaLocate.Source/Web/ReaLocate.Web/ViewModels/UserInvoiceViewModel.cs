namespace ReaLocate.Web.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;
    using ReaLocate.Web.ViewModels;

    public class UserInvoiceViewModel : IMapFrom<Invoice>
    {
        public string EncodedId { get; set; }
        public UserAsInvoiceRecepientViewModel UserRecepient { get; set; }

        public string About { get; set; }

        public string Description { get; set; }

        public int Quality { get; set; }

        public decimal TotalCost { get; set; }
    }
}