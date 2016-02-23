namespace ReaLocate.Web.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CreateAgencyViewModel : IMapTo<Agency>
    {
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }

        public int? PaymentDetailsId { get; set; }

        public bool HasPackage { get; set; }

        [Required]
        public PaymentDetailsViewModel PaymentDetails { get; set; }
    }
}