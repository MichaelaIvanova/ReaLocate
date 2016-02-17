namespace ReaLocate.Data.Models
{
    using Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RealEstate : BaseModel<int>
    {
        private ICollection<Photo> photos;
        private ICollection<Amentity> amentities;

        public RealEstate()
        {
            this.photos = new HashSet<Photo>();
            this.amentities = new HashSet<Amentity>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        // TODO: prop Expires On?

        public string PublisherId { get; set; } //user id is guid

        public virtual User Publisher { get; set; }

        public int? AgencyId { get; set; } // agency is optional, only one agency can offer each rela estatesit

        public virtual Agency Agency { get; set; }

        public int? VisitorsDetailsId { get; set; }

        public VisitorsDetails VisitorsDetails { get; set; }

        //to show it on google maps
        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual ICollection<Photo> Photos { get { return this.photos; } set { this.photos = value; } }

        public virtual ICollection<Amentity> Amentities { get { return this.amentities; } set { this.amentities = value; } }
    }
}
