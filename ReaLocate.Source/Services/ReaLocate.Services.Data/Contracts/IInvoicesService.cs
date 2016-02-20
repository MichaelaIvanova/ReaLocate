namespace ReaLocate.Services.Data.Contracts
{
    using ReaLocate.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IInvoicesService
    {
        string EncodeId(int id);

        IQueryable<Invoice> GetAll();

        Invoice GetById(int id);

        int Add(Invoice newRealInvoice);

        Invoice GetByEncodedId(string id);
    }
}
