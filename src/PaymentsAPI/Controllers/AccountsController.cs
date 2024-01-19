using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{iban}/transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetTransactions(string iban)
        {
            if(string.IsNullOrWhiteSpace(iban))
            {
                return BadRequest("IBAN cannot be null");
            }

            var list = new List<object>
            {
                new { Id = "1", Debtor = "Debtor1", Creditor = "Creditor1", Amount = 100.00, Currency = "EUR" },
                new { Id = "2", Debtor = "Debtor2", Creditor = "Creditor2", Amount = 200.00, Currency = "USD" },
                new { Id = "3", Debtor = "Debtor3", Creditor = "Creditor3", Amount = 300.00, Currency = "GBP" }
            };

            if(list?.Count == 0)
            {
                return NoContent();
            }

            return Ok(list);
        }
    }
}
