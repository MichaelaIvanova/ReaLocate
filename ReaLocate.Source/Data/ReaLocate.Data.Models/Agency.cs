namespace ReaLocate.Data.Models
{
    using Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Agency : BaseModel<int>
    {
        private ICollection<RealEstate> realEstates;
        private ICollection<User> brokers;
        private ICollection<Invoice> invoices;

        public Agency()
        {
            this.realEstates = new HashSet<RealEstate>();
            this.brokers = new HashSet<User>();
            this.invoices = new HashSet<Invoice>();
        }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public int? PackageValue { get; set; }

        public int? PaymentDetailsId { get; set; }

        public virtual PaymentDetails PaymentDetails { get; set; }

        public virtual ICollection<RealEstate> RealEstates { get { return this.realEstates; } set { this.realEstates = value; } }

        public virtual ICollection<User> Brokers { get { return this.brokers; } set { this.brokers = value; } }

        public virtual ICollection<Invoice> Invoices { get { return this.invoices; } set { this.invoices = value; } }
    }
}
