using Hawalayk_APP.Attributes;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    //[Authorize(Roles ="Craftsman")]
    [Route("api/[controller]")]
    [ApiController]
    public class CraftsmenController : ControllerBase
    {
        private readonly ICraftRepository _craftRepository;
        private readonly ICraftsmenRepository _crafsmenRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostRepository _postRepository;
        private readonly IServiceRequestRepository _serviceRequestRepository;
        // c s , oop csh , database , linq ,EF , MVc , 
        public CraftsmenController(ICraftRepository craftRepository, ICraftsmenRepository crafsmenRepository, UserManager<ApplicationUser> userManager, 
            IPostRepository postRepository, IServiceRequestRepository serviceRequestRepository)
        {
            _craftRepository = craftRepository;
            _userManager = userManager;
            _crafsmenRepository = crafsmenRepository;
            _postRepository = postRepository;
            _serviceRequestRepository = serviceRequestRepository;
        }

        [HttpGet("MyAccount")]
        public async Task<IActionResult> GetCraftsmanAccount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }
            var craftsman = await _crafsmenRepository.GetById(userId);

            if (craftsman == null)
            {
                return BadRequest("Not Allowed :");
            }
            var result = await _crafsmenRepository.GetCraftsmanAccountAsync(craftsman);
            return Ok(result);
        }


        [ServiceFilter(typeof(BlockingFilter))]
        [BlockCheck("craftsmanId")] // Specify the parameter name used in the action
        [HttpGet("CraftsmanAccount/{craftsmanId}")]
        public async Task<IActionResult> GetCraftsmanAccountById(string craftsmanId)
        {
            var craftsman = await _crafsmenRepository.GetById(craftsmanId);

            if (craftsman == null)
            {
                return BadRequest("Not Allowed :");
            }
            var result = await _crafsmenRepository.GetCraftsmanAccountAsync(craftsman);
            return Ok(result);
        }



        //done
        [HttpPut("UpdateMyAccount")]
        public async Task<IActionResult> UpdateCraftsmanAccountAsync([FromForm] CraftsmanUpdatedAccountDTO craftmanAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }

            var result = await _crafsmenRepository.UpdateCraftsmanAccountAsync(userId, craftmanAccount);

            if (!result.IsUpdated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        //done
        [HttpPut("UpdateMyProfilePic")]
        public async Task<IActionResult> UpdateCraftsmanProfilePicAsync(IFormFile profilePic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }

            var result = await _crafsmenRepository.UpdateCraftsmanProfilePicAsync(userId, profilePic);

            if (!result.IsUpdated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }



        [HttpGet("MyPortfolio")]
        public async Task<IActionResult> getPortfolio()
        {
            //string craftsmanId
            var craftsmanId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (craftsmanId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }
            var Portfolio = await _postRepository.GetGraftsmanPortfolio(craftsmanId, craftsmanId);
            if (Portfolio != null)
            {
                return Ok(Portfolio);
            }
            else
                return NotFound(new { message = "no posts yet" });

        }
        /* [HttpGet("CraftsNames")]
         public async Task<List<string>> GetCraftsNamesAsync()
         {
             return  await _craftRepository.GetAllCraftsNamesAsync();
         }
        */

        [HttpGet("GetCraftsmenOfAcraft/{craftName}")]
        public async Task<ActionResult<List<CraftsmanDTO>>> GetCraftsmenOfACraft(string craftName)
        {
            //if (!Enum.TryParse<CraftName>(craftName, out var craftNameAsEnum))
            //{
            //    return BadRequest("Invalid craftName");
            //}
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var craftsmen = await _craftRepository.GetCraftsmenOfACraft(userId, craftName);
            return craftsmen;
        }

        [HttpGet("GetNumberOfCraftsmen")]
        public async Task<IActionResult> NumberOfCraftsmen()
        {
            int counter = await _crafsmenRepository.craftsmanNumber();
            return Ok(counter);
        }


        [HttpGet("GetMyOwnPostsFromMyCraftGallary")]
        public async Task<IActionResult> GetMyOwnPostsFromMyCraftGallary()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }

            var result = await _crafsmenRepository.FilterMyCraftGallary(userId);
            return Ok(result);
        }

        [HttpGet("GetAvailableServiceRequestsByCraft")]
        public async Task<IActionResult> AvailableServiceRequestsByCraft()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in claims.");
            }

            var craftsman = await _crafsmenRepository.GetById(userId);
            if (craftsman == null)
            {
                return NotFound("Craftsman not found.");
            }

            if (craftsman.Craft == null)
            {
                return BadRequest("Craft information is missing for the craftsman.");
            }

            var craftName = craftsman.Craft.Name;
            var requests = await _serviceRequestRepository.GetAvailableServiceRequestsByCraft(craftName, userId);

            return Ok(requests);
        }




        [HttpGet("GetAcceptedServiceRequestsForCraftsman")]
        public async Task<IActionResult> GetAcceptedServiceRequestsForCraftsman()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var requests = await _crafsmenRepository.GetAcceptedServiceRequestsFromCustomersByACraftsman(userId);

            return Ok(requests);

        }


    }
}
