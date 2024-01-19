using PaymentsAPI.Models;

namespace PaymentsAPI.Services.Payments
{
    public interface IPaymentService
    {
        Task<Guid> InitiatePaymentAsync(PaymentDetailsModel paymentDetailsModel);
    }
}
