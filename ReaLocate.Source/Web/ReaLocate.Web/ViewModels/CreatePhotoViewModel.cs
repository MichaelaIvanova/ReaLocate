namespace ReaLocate.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CreatePhotoViewModel : IMapTo<Photo>
    {
        public int? RealEstateId { get; set; }

        public RealEstate RealEstate { get; set; }

        public string SourceUrl { get; set; }

        [FileExtensions(ErrorMessage = "Must choose .jpeg file.", Extensions = "jpg")]
        public HttpPostedFileBase FilePic { get; set; }
    }
}