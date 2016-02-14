namespace ReaLocate.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using ReaLocate.Data.Common;
    using ReaLocate.Data.Models;
    using Web;

    public class PhotosService : IPhotosService
    {
        private readonly IDbRepository<Photo> photos;
        private readonly IIdentifierProvider identifierProvider;

        public PhotosService(IDbRepository<Photo> photos, IIdentifierProvider identifierProvider)
        {
            this.photos = photos;
            this.identifierProvider = identifierProvider;
        }

        public int Add(Photo newPhoto)
        {
            this.photos.Add(newPhoto);
            this.photos.Save();

            return newPhoto.Id;
        }

        public string EncodeId(int id)
        {
            var stringId = this.identifierProvider.EncodeId(id);

            return stringId;
        }

        public IQueryable<Photo> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Photo GetByEncodedId(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var photo = this.photos.GetById(intId);
            return photo;
        }

        public IQueryable<Photo> GetById(int id)
        {
            return this.photos
                .All()
                .Where(c => c.Id == id);
        }
    }
}
