using PaymentsAPI.Models;
using System.Transactions;

namespace PaymentsAPI.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        public async Task<Guid> InitiatePaymentAsync(PaymentDetailsModel paymentDetailsModel)
        {
            ArgumentNullException.ThrowIfNull(paymentDetailsModel, nameof(paymentDetailsModel));

            //Simulate a 2s payment
            await Task.Delay(2000);

            //Return payment id
            return Guid.NewGuid();
        }
    }
}
