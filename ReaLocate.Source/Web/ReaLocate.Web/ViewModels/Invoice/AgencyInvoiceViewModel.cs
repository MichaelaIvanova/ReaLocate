namespace ReaLocate.Web.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;
    using ReaLocate.Web.ViewModels;

    public class AgencyInvoiceViewModel : UserInvoiceViewModel
    {
        public AgencyAsInvoiceRecepientViewModel AgencyRecepient { get; set; }
    }
}