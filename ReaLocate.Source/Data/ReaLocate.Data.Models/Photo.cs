namespace ReaLocate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Photo
    {
        public int Id { get; set; }

        public int? RealEstateId { get; set; }

        public RealEstate RealEstate { get; set; }

        public string SourceUrl { get; set; }
    }
}