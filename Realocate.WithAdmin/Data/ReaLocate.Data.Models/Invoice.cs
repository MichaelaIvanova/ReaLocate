namespace ReaLocate.Data.Models
{
    using Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Invoice : BaseModel<int>
    {
        public string UserRecepientId { get; set; }

        // as ordinary User, can be null
        public virtual User UserRecepient { get; set; }

        // as company - Agency, can be null
        //public int? AgencyRecepientId { get; set; }

        //public virtual Agency AgencyRecepient { get; set; }

        //  public DateTime CreatedOn { get; set; }

        public string About { get; set; }

        public Duration Duration { get; set; }

        public string Description { get; set; }

        public int Quality { get; set; }

        public decimal TotalCost { get; set; }
    }
}
