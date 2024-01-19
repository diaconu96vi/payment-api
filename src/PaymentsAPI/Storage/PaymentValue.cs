namespace PaymentsAPI.Storage
{
    public class PaymentValue
    {
        public DateTime Timestamp { get; set; }
        public string IBAN { get; set; } = string.Empty;
    }
}
