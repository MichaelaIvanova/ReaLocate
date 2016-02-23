namespace ReaLocate.Web.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;

    public class PaymentDetailsViewModel : IMapTo<PaymentDetails>, IMapFrom<PaymentDetails>
    {
        public int VatNumber { get; set; }

        public PaymentType PaymentType { get; set; }

        public Duration Duration { get; set; }
    }
}