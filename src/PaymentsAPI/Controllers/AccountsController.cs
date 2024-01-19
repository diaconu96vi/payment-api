using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentsAPI.Services.Payments;
using PaymentsAPI.Services.Transactions;
using System.Transactions;

namespace PaymentsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConcurrencyChecker _concurrencyChecker;
        private readonly ITransactionService _transactionService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IConcurrencyChecker concurrencyChecker, ITransactionService transactionService, ILogger<AccountsController> logger)
        {
            _concurrencyChecker = concurrencyChecker ?? throw new ArgumentNullException(nameof(concurrencyChecker));
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{iban}/transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTransactions(string iban)
        {
            if(string.IsNullOrWhiteSpace(iban))
            {
                _logger.LogError("IBAN cannot be null");
                return BadRequest();
            }

            try
            {
                var transactions = await _transactionService.GetTransactionsAsync(iban);

                if (transactions?.Count() > 0)
                {
                    return Ok(transactions);
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Could not get transactions");
                return BadRequest();
            }
        }
    }
}
