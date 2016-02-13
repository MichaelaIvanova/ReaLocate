namespace ReaLocate.Web.ViewModels
{
    using Microsoft.AspNet.Identity;
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;

    public class RealEstateViewModel : IMapFrom<RealEstate>, IMapTo<RealEstate> // IHaveCustomMappings
    {
        public string Address { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public string PublisherId { get { return System.Web.HttpContext.Current.User.Identity.GetUserId(); } }
    }
}