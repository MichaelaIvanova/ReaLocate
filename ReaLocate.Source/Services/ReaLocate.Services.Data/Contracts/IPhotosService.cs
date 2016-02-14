namespace ReaLocate.Services.Data.Contracts
{
    using System.Linq;
    using ReaLocate.Data.Models;

    public interface IPhotosService
    {
        string EncodeId(int id);

        IQueryable<Photo> GetAll(int skip, int take);

        IQueryable<Photo> GetById(int id);

        int Add(Photo newRealEstate);

        Photo GetByEncodedId(string id);
    }
}
