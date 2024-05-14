using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
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
        // c s , oop csh , database , linq ,EF , MVc , 
        public CraftsmenController(ICraftRepository craftRepository, ICraftsmenRepository crafsmenRepository, UserManager<ApplicationUser> userManager, IPostRepository postRepository)
        {
            _craftRepository = craftRepository;
            _userManager = userManager;
            _crafsmenRepository = crafsmenRepository;
            _postRepository = postRepository;
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


        // [ServiceFilter(typeof(BlockingFilter))]
        [HttpGet("CraftsmanAccount")]
        public async Task<IActionResult> ShawCraftsmanAccount(string craftsmanId)
        {
            //  var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (userId == null)
            //{
            //    return NotFound("This Token Is Not Found : ");
            //}
            var craftsman = await _crafsmenRepository.GetById(craftsmanId);

            if (craftsman == null)
            {
                return BadRequest("Not Allowed :");
            }
            var result = await _crafsmenRepository.GetCraftsmanAccountAsync(craftsman);
            return Ok(result);
        }




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

        [HttpGet("MyPortfolio")]
        public async Task<IActionResult> getPortfolio()
        {
            //string craftsmanId
            var craftsmanId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (craftsmanId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }
            var Portfolio = await _postRepository.GetGraftsmanPortfolio(craftsmanId);
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

        [HttpGet("CraftsmenOfAcraft")]
        public async Task<ActionResult<List<CraftsmanDTO>>> GetCraftsmenOfACraft(string craftName)
        {
            //if (!Enum.TryParse<CraftName>(craftName, out var craftNameAsEnum))
            //{
            //    return BadRequest("Invalid craftName");
            //}

            var craftsmen = await _craftRepository.GetCraftsmenOfACraft(craftName);
            return craftsmen;
        }

        [HttpGet]
        public async Task<IActionResult> numberOfCraftsmen()
        {
            int counter = await _crafsmenRepository.craftsmanNumber();
            return Ok(counter);
        }


        [HttpGet("FilterMyCraftGallary")]
        public async Task<IActionResult> MyCraftGallary()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }

            var result = await _crafsmenRepository.FilterMyCraftGallary(userId);
            return Ok(result);
        }
        [HttpGet("Get Service Requests By CraftName")]
        public IActionResult RequestsByCraftName(CraftName craft)
        {

            return Ok(_crafsmenRepository.GetServiceRequestsByCraftName(craft));

        }


    }
}
