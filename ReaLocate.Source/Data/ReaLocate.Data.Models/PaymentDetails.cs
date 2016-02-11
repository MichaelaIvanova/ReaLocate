namespace ReaLocate.Data.Models
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PaymentDetails
    {
        public int Id { get; set; }

        public int VatNumber { get; set; }

        public PaymentType PaymentType { get; set; }

    }
}
