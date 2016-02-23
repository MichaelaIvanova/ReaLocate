using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReaLocate.Web.ViewModels
{
    public class SearchedRealEstateViewModel:DetailsRealEstateViewModel
    {
        public string Country { get; set; }

        public string City { get; set; }
    }
}