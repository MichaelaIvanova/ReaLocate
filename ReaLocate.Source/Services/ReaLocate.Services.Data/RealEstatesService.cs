namespace ReaLocate.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;
    using ReaLocate.Data.Common;
    using ReaLocate.Data.Models;
    using Web;

    public class RealEstatesService : IRealEstatesService
    {
        private readonly IDbRepository<RealEstate> realEstates;
        private readonly IIdentifierProvider identifierProvider;

        public RealEstatesService(IDbRepository<RealEstate> realEstates, IIdentifierProvider identifierProvider)
        {
            this.realEstates = realEstates;
            this.identifierProvider = identifierProvider;
        }

        public int Add(RealEstate newRealEstate, string userId, int? agencyId = default(int?))
        {
            newRealEstate.PublisherId = userId;
            newRealEstate.AgencyId = agencyId;

            this.realEstates.Add(newRealEstate);
            this.realEstates.Save();

            return newRealEstate.Id;
        }

        public IQueryable<RealEstate> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public RealEstate GetByEncodedId(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RealEstate> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
