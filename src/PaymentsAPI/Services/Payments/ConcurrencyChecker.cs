using PaymentsAPI.Storage;

namespace PaymentsAPI.Services.Payments
{
    public class ConcurrencyChecker : IConcurrencyChecker
    {
        private readonly PaymentTracker _paymentTracker;

        public ConcurrencyChecker(PaymentTracker paymentTracker)
        {
            _paymentTracker = paymentTracker ?? throw new ArgumentNullException(nameof(paymentTracker));
        }

        public void AddClientProcessing(string clientId, string iban)
        {
            ArgumentNullException.ThrowIfNull(clientId, nameof(clientId));
            ArgumentNullException.ThrowIfNull(iban, nameof(iban));

            _paymentTracker.AddPaymentInfo(clientId, iban);
        }

        public IEnumerable<PaymentValue> GetTimestamp(string iban)
        {
            ArgumentNullException.ThrowIfNull(iban, nameof(iban));

            return _paymentTracker.GetTimestamps(iban);
        }

        public bool IsClientProcessing(string clientId)
        {
            ArgumentNullException.ThrowIfNull(clientId, nameof(clientId));

            return _paymentTracker.TryGetClientTimestamp(clientId);
        }

        public void RemoveClientProcessing(string clientId)
        {
            ArgumentNullException.ThrowIfNull(clientId, nameof(clientId));

            _paymentTracker.RemovePaymentInfo(clientId);
        }
    }
}
