namespace ReaLocate.Data.Models
{
    using Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PaymentDetails : BaseModel<int>
    {
        public int VatNumber { get; set; }

        public PaymentType PaymentType { get; set; }

        public Duration Duration { get; set; }
    }
}
