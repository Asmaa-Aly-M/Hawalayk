using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult numberOfCustomer()
        {
            int counter = customerRepo.customerNumber();
            return Ok(counter);
        }
    }
}
