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
    public class PaymentDetailsService : IPaymentDetailsService
    {
        private readonly IDbRepository<PaymentDetails> paymentDetails;

        public PaymentDetailsService(IDbRepository<PaymentDetails> paymentDetails)
        {
            this.paymentDetails = paymentDetails;
        }

        public int Add(PaymentDetails newPay)
        {
            this.paymentDetails.Add(newPay);
            this.paymentDetails.Save();

            return newPay.Id;
        }

        public string EncodeId(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PaymentDetails> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public PaymentDetails GetByEncodedId(string id)
        {
            throw new NotImplementedException();
        }

        public PaymentDetails GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
