namespace ReaLocate.Services.Data.Contracts
{
    using System.Linq;
    using ReaLocate.Data.Models;

    public interface IAgenciesService
    {
        string EncodeId(int id);

        IQueryable<Agency> GetAll(int skip, int take);

        Agency GetById(int id);

        int Add(Agency newRealEstate);

        Agency GetByEncodedId(string id);
    }
}