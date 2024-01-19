using PaymentsAPI.Storage;

namespace PaymentsAPI.Services.Payments
{
    public interface IConcurrencyChecker
    {
        IEnumerable<PaymentValue> GetTimestamp(string iban);
        bool IsClientProcessing(string clientId);
        void AddClientProcessing(string clientId, string iban);
        void RemoveClientProcessing(string clientId);
    }
}
