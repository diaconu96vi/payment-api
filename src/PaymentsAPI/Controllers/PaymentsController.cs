using Microsoft.AspNetCore.Mvc;

namespace PaymentsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(ILogger<PaymentsController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Initiate()
    {
        var paymentId = Guid.NewGuid();
        return CreatedAtAction(nameof(Initiate), paymentId);
    }
}
