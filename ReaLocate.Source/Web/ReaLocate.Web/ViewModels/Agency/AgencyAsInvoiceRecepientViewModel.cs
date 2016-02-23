namespace ReaLocate.Web.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;
    using ReaLocate.Web.ViewModels;

    public class AgencyAsInvoiceRecepientViewModel : IMapFrom<Agency>
    {
        public string Name { get; set; }


        public UserAsInvoiceRecepientViewModel Owner { get; set; }

        public PaymentDetailsViewModel PaymentDetails { get; set; }
    }
}