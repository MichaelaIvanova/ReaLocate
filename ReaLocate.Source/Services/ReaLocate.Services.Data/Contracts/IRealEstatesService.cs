namespace ReaLocate.Services.Data.Contracts
{
    using ReaLocate.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRealEstatesService
    {
        IQueryable<RealEstate> GetAll(int skip, int take);

        IQueryable<RealEstate> GetById(int id);

        int Add(RealEstate newRealEstate, string userId, int? agencyId = null);

        RealEstate GetByEncodedId(string id);
    }
}
