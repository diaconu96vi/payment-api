using PaymentsAPI.Models;

namespace PaymentsAPI.Services.Transactions
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionModel>> GetTransactionsAsync(string iban);
    }
}
