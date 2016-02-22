namespace ReaLocate.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Geocoding.Google;
    using Microsoft.AspNet.Identity;
    using ReaLocate.Services.Data;
    using Services.Data.Contracts;
    using ViewModels;

    public class RealEstateCreateUtil : IRealEstateCreateUtil
    {
        private readonly IPhotosService photosService;

        public RealEstateCreateUtil()
        {
        }

        public RealEstateCreateUtil(IPhotosService photosService)
        {
            this.photosService = photosService;
        }


        public GoogleAddress GetRealAddress(CreateRealEstateViewModel realEstate)
        {
            var geocoder = new GoogleGeocoder();

            if (realEstate.Address != null && realEstate.Address.Length > 5)
            {
                List<GoogleAddress> addresses = geocoder.Geocode(realEstate.Address).ToList();
                var fullAddress = addresses[0];
                return fullAddress;
            }

            return null;
        }

        public void SavePhoto(RealEstate dbRealEstate, string realEstateEncodedId, HttpPostedFileBase photo)
        {
            if (photo != null && photo.ContentLength > 0 && photo.ContentLength < (1 * 1024 * 1024) && photo.ContentType == "image/jpeg")
            {
                string directory = HttpContext.Current.Server.MapPath("~/UploadedFiles/RealEstatePhotos/") + realEstateEncodedId;

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string filename = Guid.NewGuid().ToString() + ".jpg";
                string path = directory + "/" + filename;
                string url = "~/UploadedFiles/RealEstatePhotos/" + realEstateEncodedId + "/" + filename;
                photo.SaveAs(path);
                var newPhoto = new Photo
                {
                    SourceUrl = url,
                    RealEstate = dbRealEstate
                };

                this.photosService.Add(newPhoto);
            }
        }
    }
}