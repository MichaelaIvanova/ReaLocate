using ReaLocate.Data.Models;
using ReaLocate.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReaLocate.Web.Areas.Administration.ViewModels
{
    public class AdminRealEstateViewModel : IMapFrom<RealEstate>, IMapTo<RealEstate>
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Desciption { get; set; }

        public decimal? Price { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public OfferType OfferType { get; set; }

        public RealEstateType RealEstateType { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}