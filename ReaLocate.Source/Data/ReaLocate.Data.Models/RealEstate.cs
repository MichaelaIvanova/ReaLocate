namespace ReaLocate.Data.Models
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RealEstate
    {
        private ICollection<Photo> photos;
        private readonly DateTime createdOn;

        public RealEstate()
        {
            this.photos = new HashSet<Photo>();
            this.createdOn = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        // TODO: prop Expires On?
        public DateTime DatePublished { get { return this.createdOn; } }

        public string PublisherId { get; set; } //user id is guid

        public virtual User Publisher { get; set; }

        public int? AgencyId { get; set; } // agency is optional, only one agency can offer each rela estatesit

        public virtual Agency Agency { get; set; }

        //to show it on google maps
        public string Address { get; set; }

        public virtual ICollection<Photo> Photos { get { return this.photos; } set { this.photos = value; } }
    }
}
