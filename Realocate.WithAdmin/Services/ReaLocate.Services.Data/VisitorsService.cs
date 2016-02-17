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
    public class VisitorsService : IVisitorsService
    {
        private readonly IRepository<VisitorsDetails> visitorsDetails;

        public VisitorsService(IRepository<VisitorsDetails> visitorsDetails)
        {
            this.visitorsDetails = visitorsDetails;
        }

        public int Add(VisitorsDetails details)
        {
            this.visitorsDetails.Add(details);
            this.visitorsDetails.SaveChanges();

            return details.Id;
        }

        public IQueryable<VisitorsDetails> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public VisitorsDetails GetById(int id)
        {
            return this.visitorsDetails
                .All()
                .Where(c => c.Id == id)
                .First();
        }

        public void Update(VisitorsDetails details)
        {
            this.visitorsDetails.Update(details);

            this.visitorsDetails.SaveChanges();
        }
    }
}
