namespace ReaLocate.Web.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;

    public class DetailsAgencyViewModel : IMapFrom<Agency>
    {
        public int Id { get; set; }

        public string EncodedId { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public int? PaymentDetailsId { get; set; }

        public PaymentDetailsViewModel PaymentDetails { get; set; }
    }
}