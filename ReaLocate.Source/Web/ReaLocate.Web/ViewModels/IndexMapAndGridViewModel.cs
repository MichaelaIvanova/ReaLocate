using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReaLocate.Web.ViewModels
{
    public class IndexMapAndGridViewModel
    {
        public IEnumerable<CoordinateViewModel> MapsCoordinates { get; set; }

        public IEnumerable<DetailsRealEstateViewModel> Estates { get; set; }
        public int TotalPages { get; internal set; }
        public int CurrentPage { get; internal set; }
    }
}