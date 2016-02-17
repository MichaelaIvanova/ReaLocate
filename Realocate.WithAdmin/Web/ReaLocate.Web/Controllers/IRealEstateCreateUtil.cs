using System.Web;
using Geocoding.Google;
using ReaLocate.Data.Models;
using ReaLocate.Web.ViewModels;

namespace ReaLocate.Web.Controllers
{
    public interface IRealEstateCreateUtil
    {
        GoogleAddress GetRealAddress(CreateRealEstateViewModel realEstate);
        void SavePhoto(RealEstate dbRealEstate, string realEstateEncodedId, HttpPostedFileBase photo);
    }
}