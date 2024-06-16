using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICraftRepository _craftRepository;

        public CustomerController(ICustomerRepository customerRepository, ICraftRepository craftRepository)
        {
            _customerRepository = customerRepository;
            _craftRepository = craftRepository;
        }


        [HttpGet("GetCustomerAccount")]
        public async Task<IActionResult> GetCustomerAccount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }
            var customer = await _customerRepository.GetByIdAsync(userId);

            if (customer == null)
            {
                return BadRequest("Not Allowed :");
            }
            var result = await _customerRepository.GetCustomerAccountAsync(customer);
            return Ok(result);
        }


        [HttpGet("NumberOfCustomer")]
        public async Task<IActionResult> numberOfCustomer()
        {
            int counter = await _customerRepository.customerNumber();
            return Ok(counter);
        }


        /* [HttpGet("Get All Service Requests Done by a customer")]
         public async Task<IActionResult> AllRequestsForThisCustomer()
         {
             var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
             var requests = await _customerRepository.GetServiceRequestsForThisCustomer(userId);
             return Ok(requests);

         }
        هنركن دى هنا لغاية لما نشوف هنحتاجها ولا لا لان محدش عارف بس محدش يمسحهاهى بترجع كل ال service ا عملها العميل*/

        [HttpGet("SearchAboutCraftsmen")]
        public async Task<IActionResult> SearchAboutCraftsmen(string craftName, string gonernorate) 
        {
            var craftNameEnumValue= await _craftRepository.GetEnumValueOfACraftByArabicDesCription(craftName);
            var craftsmen = await _customerRepository.searchAboutCraftsmen(craftNameEnumValue, gonernorate);
           return Ok(craftsmen);
        }

    }
}
