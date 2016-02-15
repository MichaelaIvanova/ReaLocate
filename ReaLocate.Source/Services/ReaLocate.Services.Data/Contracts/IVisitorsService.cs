namespace ReaLocate.Services.Data.Contracts
{
    using ReaLocate.Data.Models;
    using System.Linq;

    public interface IVisitorsService
    {
        IQueryable<VisitorsDetails> GetAll(int skip, int take);

        IQueryable<VisitorsDetails> GetById(int id);

        int Add(VisitorsDetails details);
    }
}
