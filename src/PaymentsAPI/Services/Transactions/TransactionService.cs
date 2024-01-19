using PaymentsAPI.Models;
using PaymentsAPI.Services.Payments;

namespace PaymentsAPI.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly IConcurrencyChecker _concurrencyChecker;

        public TransactionService(IConcurrencyChecker concurrencyChecker)
        {
            _concurrencyChecker = concurrencyChecker ?? throw new ArgumentNullException(nameof(concurrencyChecker));
        }
        public async Task<IEnumerable<TransactionModel>> GetTransactionsAsync(string iban)
        {
            //Simulate retrieving transactions
            var dbTransactions = await GetTransactionsByIBANAsync(iban);
            var processingIbans = _concurrencyChecker.GetTimestamp(iban);

            if(processingIbans?.Count() > 0)
            {
                return dbTransactions.Where(transaction => !processingIbans.Any(y => y.IBAN == transaction.CreditorAccount) ||
                    processingIbans.Any(p => p.IBAN == transaction.CreditorAccount && (DateTime.UtcNow - p.Timestamp).TotalSeconds >= 2));
            }

            return dbTransactions;
        }

        #region Private methods
        private Task<IEnumerable<TransactionModel>> GetTransactionsByIBANAsync(string iban)
        {
            var paymentList = new List<TransactionModel>
            {
                new TransactionModel
                {
                    PaymentId = Guid.NewGuid(),
                    DebtorAccount = "DE12345678901234567890",
                    CreditorAccount = "GB98765432109876543210",
                    InstructedAmount = 100.50m,
                    Currency = "EUR"
                },
                new TransactionModel
                {
                    PaymentId = Guid.NewGuid(),
                    DebtorAccount = "FR98765432109876543210",
                    CreditorAccount = "IT12345678901234567890",
                    InstructedAmount = 50.75m,
                    Currency = "USD"
                },
                new TransactionModel
                {
                    PaymentId = Guid.NewGuid(),
                    DebtorAccount = "ES34567890123456789012",
                    CreditorAccount = "DE56789012345678901234",
                    InstructedAmount = 75.20m,
                    Currency = "GBP"
                }
            };
            var filteredPayments = paymentList.Where(transaction => transaction.CreditorAccount == iban);
            return Task.FromResult(filteredPayments);
        }
        #endregion
    }
}
