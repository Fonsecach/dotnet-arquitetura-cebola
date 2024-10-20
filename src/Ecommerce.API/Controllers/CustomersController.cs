using System;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerApplicationService _customerApplicationService;

    public CustomersController(ICustomerApplicationService customerApplicationService)
    {
        _customerApplicationService = customerApplicationService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateCustomer([FromBody] CustomerDto customerDto)
    {
        try
        {
            _customerApplicationService.SaveCustomer(customerDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        return Created("", customerDto);
    }
}
