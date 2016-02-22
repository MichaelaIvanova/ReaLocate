namespace ReaLocate.Services.Data.Contracts
{
    using System.Linq;
    using ReaLocate.Data.Models;

    public interface IRealEstatesService
    {
        string EncodeId(int id);

        IQueryable<RealEstate> GetAll();

        IQueryable<RealEstate> GetAllForPaging(int skip, int take);

        IQueryable<RealEstate> GetById(int id);

        int Add(RealEstate newRealEstate);

        RealEstate GetByEncodedId(string id);

        void Update(RealEstate realEstate);

        void Delete(RealEstate realEstateFromDb);
    }
}
