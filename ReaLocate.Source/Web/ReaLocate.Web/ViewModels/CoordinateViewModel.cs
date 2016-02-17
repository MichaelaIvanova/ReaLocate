namespace ReaLocate.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class CoordinateViewModel
    {
        public double? GeoLong { get; set; }

        public double? GeoLat { get; set; }

        public string Address { get; set; }
    }
}