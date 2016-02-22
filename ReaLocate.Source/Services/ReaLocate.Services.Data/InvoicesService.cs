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
    public class InvoicesService : IInvoicesService
    {
        private readonly IRepository<Invoice> invoices;
        private readonly IIdentifierProvider identifierProvider;

        public InvoicesService(IRepository<Invoice> invoices, IIdentifierProvider identifierProvider)
        {
            this.invoices = invoices;
            this.identifierProvider = identifierProvider;
        }

        public int Add(Invoice newInvoice)
        {
            this.invoices.Add(newInvoice);
            this.invoices.SaveChanges();

            return newInvoice.Id;
        }

        public string EncodeId(int id)
        {
            var stringId = this.identifierProvider.EncodeId(id);

            return stringId;
        }

        public IQueryable<Invoice> GetAll()
        {
            return this.invoices
                 .All();
        }

        public Invoice GetByEncodedId(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var invoice = this.invoices.GetById(intId);
            return invoice;
        }

        public Invoice GetById(int id)
        {
            return this.invoices
                .All()
                .Where(c => c.Id == id).First();
        }
    }
}
