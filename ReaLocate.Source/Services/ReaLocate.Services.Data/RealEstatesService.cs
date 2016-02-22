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
        private readonly IRepository<RealEstate> realEstates;
        private readonly IIdentifierProvider identifierProvider;

        public RealEstatesService(IRepository<RealEstate> realEstates, IIdentifierProvider identifierProvider)
        {
            this.realEstates = realEstates;
            this.identifierProvider = identifierProvider;
        }

        public int Add(RealEstate newRealEstate)
        {

            this.realEstates.Add(newRealEstate);
            this.realEstates.SaveChanges();

            return newRealEstate.Id;
        }

        public IQueryable<RealEstate> GetAllForPaging(int skip, int take)
        {
            return this.realEstates.All().OrderByDescending(c=>c.CreatedOn)
                .Skip(skip).Take(take);
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

        public void Update(RealEstate realEstate)
        {
            this.realEstates.Update(realEstate);
            this.realEstates.SaveChanges();
        }

        public void Delete(RealEstate realEstateFromDb)
        {
            this.realEstates.Delete(realEstateFromDb);

        }
    }
}
