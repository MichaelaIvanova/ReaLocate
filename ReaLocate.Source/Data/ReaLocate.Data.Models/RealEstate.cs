namespace ReaLocate.Data.Models
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RealEstate
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PublisherId { get; set; } //user id is guid

        public virtual User Publisher { get; set; }

        public int? AgencyId { get; set; } // agency is optional, only one agency can offer each rela estatesit

        public virtual Agency Agency { get; set; }
    }
}
