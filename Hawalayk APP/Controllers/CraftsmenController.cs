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
        // c s , oop csh , database , linq ,EF , MVc , 
        public CraftsmenController(ICraftRepository craftRepository, ICraftsmenRepository crafsmenRepository, UserManager<ApplicationUser> userManager)
        {
            _craftRepository = craftRepository;
            _userManager = userManager;
            _crafsmenRepository = crafsmenRepository;
        }

        [HttpGet("MyAccount")]
        public async Task<IActionResult> GetCraftsmanAccount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }
            var craftsman = _crafsmenRepository.GetById(userId);

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
        public IActionResult numberOfCraftsmen()
        {
            int counter = _crafsmenRepository.craftsmanNumber();
            return Ok(counter);
        }


    }
}
