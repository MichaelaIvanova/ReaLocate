namespace ReaLocate.Web.ViewModels
{
    using Microsoft.AspNet.Identity;
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CreateRealEstateViewModel : IMapTo<RealEstate> // IHaveCustomMappings
    {
        [Required]
        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(300)]
        public string Description { get; set; }

        public decimal? Price { get; set; }

        public OfferType OfferType { get; set; }

        public RealEstateType RealEstateType { get; set; }

        public string PublisherId { get { return System.Web.HttpContext.Current.User.Identity.GetUserId(); } }

    }
}