namespace ReaLocate.Services.Data
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ReaLocate.Data.Models;
    using ReaLocate.Data.Common;
    using Web;
    public class AgenciesService : IAgenciesService
    {
        private readonly IRepository<Agency> agencies;
        private readonly IIdentifierProvider identifierProvider;

        public AgenciesService(IRepository<Agency> agencies, IIdentifierProvider identifierProvider)
        {
            this.agencies = agencies;
            this.identifierProvider = identifierProvider;
        }

        public int Add(Agency newAgency)
        {
            this.agencies.Add(newAgency);
            this.agencies.SaveChanges();

            return newAgency.Id;
        }

        public void Delete(Agency agencyFromDb)
        {
            this.agencies.Delete(agencyFromDb);
        }

        public string EncodeId(int id)
        {
            var stringId = this.identifierProvider.EncodeId(id);

            return stringId;
        }

        public IQueryable<Agency> GetAll()
        {
           return this.agencies.All();
        }

        public Agency GetByEncodedId(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var agency = this.agencies.GetById(intId);
            return agency;
        }

        public Agency GetById(int id)
        {
            return this.agencies
                .All()
                .Where(c => c.Id == id).First();
        }
    }
}
