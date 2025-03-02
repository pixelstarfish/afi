using Microsoft.AspNetCore.Mvc;

namespace Afi.CustomerPortal.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerRegistrationController : ControllerBase
{
    private readonly ILogger<CustomerRegistrationController> _logger;

    public CustomerRegistrationController(ILogger<CustomerRegistrationController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public void Post()
    {
    }
}