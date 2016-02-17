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

        public int Add(RealEstate newRealEstate)
        {

            this.realEstates.Add(newRealEstate);
            this.realEstates.Save();

            return newRealEstate.Id;
        }

        public IQueryable<RealEstate> GetAllForPaging(int skip=0, int take=10)
        {
            return this.realEstates.All().Skip(skip).Take(take);
        }

        public RealEstate GetByEncodedId(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var realEstate = this.realEstates.GetById(intId);
            return realEstate;
        }

        public IQueryable<RealEstate> GetById(int id)
        {
            return this.realEstates
                .All()
                .Where(c => c.Id == id);
        }

        public string EncodeId(int id)
        {
            var stringId = this.identifierProvider.EncodeId(id);

            return stringId;
        }

        public IQueryable<RealEstate> GetAll()
        {
            return this.realEstates
                 .All()
                 .OrderByDescending(c => c.CreatedOn);
        }
    }
}
