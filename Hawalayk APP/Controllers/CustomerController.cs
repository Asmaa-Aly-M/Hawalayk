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
        ICraftRepository craftrepo;

        public CustomerController(ICustomerRepository _customerRepo, ICraftRepository _craftrepo)
        {
            customerRepo = _customerRepo;
            craftrepo = _craftrepo;
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
        [HttpGet("Get All Service Requests by customer")]
        public async Task<IActionResult> AllRequestsForThisCustomer()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var requests = await customerRepo.GetServiceRequestsForThisCustomer(userId);
            return Ok(requests);

        }

        [HttpGet("searchAboutCraftsmen")]
        public async Task<IActionResult> searchAboutCraftsmen(string craftName, string gonernorate) 
        {
            var craftNameEnumValue= await craftrepo.GetEnumValueOfACraftByArabicDesCription(craftName);
            var craftsmen = await customerRepo.searchAboutCraftsmen(craftNameEnumValue, gonernorate);
           return Ok(craftsmen);
        }

    }
}
