namespace ReaLocate.Services.Data.Contracts
{
    using ReaLocate.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPaymentDetailsService
    {
        string EncodeId(int id);

        IQueryable<PaymentDetails> GetAll(int skip, int take);

        PaymentDetails GetById(int id);

        int Add(PaymentDetails paymentDetails);

        PaymentDetails GetByEncodedId(string id);
    }
}
