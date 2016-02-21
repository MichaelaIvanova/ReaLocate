namespace ReaLocate.Web.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;
    using System.Web.Mvc;
    public class CreateAgencyViewModel : IMapTo<Agency>
    {
        public string Name { get; set; }

        public int? PaymentDetailsId { get; set; }

        public bool HasPackage { get; set; }

        public PaymentDetailsViewModel PaymentDetails { get; set; }
    }
}