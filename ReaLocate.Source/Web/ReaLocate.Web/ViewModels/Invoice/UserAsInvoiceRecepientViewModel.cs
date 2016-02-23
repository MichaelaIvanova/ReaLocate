namespace ReaLocate.Web.ViewModels
{
    using ReaLocate.Data.Models;
    using ReaLocate.Web.Infrastructure.Mapping;
    using ReaLocate.Web.ViewModels;
    using System.ComponentModel.DataAnnotations;

    public class UserAsInvoiceRecepientViewModel : IMapFrom<User>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public virtual PaymentDetailsViewModel PaymentDetails { get; set; }

    }
}