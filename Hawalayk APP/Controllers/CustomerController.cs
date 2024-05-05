using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerRepository customerRepo;

        public CustomerController(ICustomerRepository _customerRepo)
        {
            customerRepo = _customerRepo;
        }


        [HttpGet("ShowCustomerAccount")]
        public async Task<IActionResult> GetCustomerAccount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }
            var customer = await customerRepo.GetByIdAsync(userId);

            if (customer == null)
            {
                return BadRequest("Not Allowed :");
            }
            var result = await customerRepo.GetCustomerAccountAsync(customer);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> numberOfCustomer()
        {
            int counter = await customerRepo.customerNumber();
            return Ok(counter);
        }
    }
}
