using ReaLocate.Data.Models;
using ReaLocate.Web.Infrastructure.Mapping;
using ReaLocate.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReaLocate.Web.Controllers
{

    public class InvoiceByUserViewModel:IMapTo<Invoice>
    {
        public string RealEstateId { get; set; }

        public string About { get; set; }

        public string Description { get; set; }

        public int Quality { get; set; }

        public decimal TotalCost { get; set; }

        public PaymentDetailsViewModel PaymentDetails { get; set; }
    }
}