namespace ReaLocate.Web.ViewModels
{
    using System.Collections.Generic;
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;

    public class DetailsRealEstateViewModel : IMapFrom<RealEstate>
    {
        public int Id { get; set; }

        public string EncodedId { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public OfferType OfferType { get; set; }

        public RealEstateType RealEstateType { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}