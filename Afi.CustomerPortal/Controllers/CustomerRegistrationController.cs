using Afi.CustomerPortal.Entities.Dto;
using Afi.CustomerPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Afi.CustomerPortal.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerRegistrationController(ICustomerRegistrationService customerRegistrationService) : ControllerBase
{
    private readonly ICustomerRegistrationService _customerRegistrationService = customerRegistrationService;

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult Post(CustomerRegistration value)
    {
        int customerId = _customerRegistrationService.Register(value);

        return customerId > 0 ?
            Results.Created($"/customer/{customerId}", customerId) : Results.Conflict();
    }
}