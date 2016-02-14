using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReaLocate.Web.ViewModels
{
    public class CreateRealEstateAndPhotoViewModel
    {
        public CreateRealEstateViewModel RealEstate { get; set; }

        public IList<CreatePhotoViewModel> RealEstatePhotos { get; set; }
    }
}