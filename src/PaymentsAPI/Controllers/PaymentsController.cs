using Microsoft.AspNetCore.Mvc;
using PaymentsAPI.Models;
using PaymentsAPI.Services.Payments;

namespace PaymentsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IConcurrencyChecker _concurrencyChecker;
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentsController> _logger;
    private const string ClientIdHeader = "Client-ID";

    public PaymentsController(IConcurrencyChecker concurrencyChecker, IPaymentService paymentService, ILogger<PaymentsController> logger)
    {
        _concurrencyChecker = concurrencyChecker ?? throw new ArgumentNullException(nameof(concurrencyChecker));
        _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Initiate([FromBody]PaymentDetailsModel paymentDetailsModel)
    {
        if(paymentDetailsModel == null)
        {
            _logger.LogError("Payment details cannot be null");
            return BadRequest();
        }

        if(!Request.Headers.TryGetValue(ClientIdHeader, out var requestId))
        {
            _logger.LogError($"Could not find {ClientIdHeader} in request headers");
            return BadRequest();
        }

        var clientId = requestId.ToString();
        if(_concurrencyChecker.IsClientProcessing(clientId))
        {
            _logger.LogError($"Client with id {clientId} is already processing a payment");
            return Conflict();
        }

        try
        {
            _concurrencyChecker.AddClientProcessing(clientId, paymentDetailsModel.CreditorAccount);
            var paymentId = await _paymentService.InitiatePaymentAsync(paymentDetailsModel);
            _concurrencyChecker.RemoveClientProcessing(clientId);
            return CreatedAtAction(nameof(Initiate), paymentId);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Could not initiate payment");
            _concurrencyChecker.RemoveClientProcessing(clientId);
            return BadRequest();
        }
    }
}
