namespace ReaLocate.Data.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    public class User : IdentityUser
    {
        private ICollection<RealEstate> realEstates;
        private ICollection<Invoice> invoices;

        public User()
        {
            this.realEstates = new HashSet<RealEstate>();
            this.invoices = new HashSet<Invoice>();
        }

        public virtual ICollection<RealEstate> RealEstates { get { return this.realEstates; } set { this.realEstates = value; } }

        public virtual ICollection<Invoice> Invoices { get { return this.invoices; } set { this.invoices = value; } }

        public bool IsOnline { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePicturePath { get; set; }

        // User can own agency
        public int? MyOwnAgencyId { get; set; }

        public virtual Agency MyOwnAgency { get; set; }

        // User can work for agency
        public int? AgencyWorkForId { get; set; }

        public virtual Agency AgencyWorkFor { get; set; }

        public int? PaymentDetailsId { get; set; }

        public virtual PaymentDetails PaymentDetails { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
