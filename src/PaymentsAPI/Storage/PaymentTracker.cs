namespace PaymentsAPI.Storage
{
    public class PaymentTracker
    {
        private static readonly object _lockObject = new();

        private Dictionary<string, PaymentValue> _paymentsTimestampDictionary = new Dictionary<string, PaymentValue>();

        public IEnumerable<PaymentValue> GetTimestamps(string iban)
        {             
            lock (_lockObject)
            {
                return _paymentsTimestampDictionary.Where(x => x.Value.IBAN == iban).Select(x => x.Value);
            }
        }
        public bool TryGetClientTimestamp(string clientId)
        {
            lock (_lockObject)
            {
                return _paymentsTimestampDictionary.TryGetValue(clientId, out _);
            }
        }

        public void AddPaymentInfo(string clientId, string iban)
        {
            lock (_lockObject)
            {
                var value = new PaymentValue
                {
                    Timestamp = DateTime.UtcNow,
                    IBAN = iban
                };

                if (!_paymentsTimestampDictionary.ContainsKey(clientId))
                {
                    _paymentsTimestampDictionary.Add(clientId, value);
                    return;
                }

                _paymentsTimestampDictionary[clientId] = value;
            }
        }

        public void RemovePaymentInfo(string clientId)
        {
            lock (_lockObject)
            {
                if(!_paymentsTimestampDictionary.ContainsKey(clientId))
                {
                    return;
                }

                _paymentsTimestampDictionary.Remove(clientId);
            }
        }
    }
}
