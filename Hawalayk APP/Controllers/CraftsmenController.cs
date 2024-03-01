using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    //[Authorize(Roles ="Craftsman")]
    [Route("api/[controller]")]
    [ApiController]
    public class CraftsmenController : ControllerBase
    {
        IPostRepository postRepo;
        private readonly ICraftRepository _craftRepository;
        private readonly ICraftsmenRepository _crafsmenRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CraftsmenController(IPostRepository _postRepo, ICraftRepository craftRepository, ICraftsmenRepository crafsmenRepository, UserManager<ApplicationUser> userManager) 
        {
            postRepo= _postRepo;
            _craftRepository= craftRepository;
            _userManager= userManager;
            _crafsmenRepository= crafsmenRepository;
        }

        [HttpGet("ShowCraftsmanAccount")]
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
        //[HttpPut("UpdateCraftsmanAccount")]
        //public async Task<IActionResult> UpdateCraftsmanAccountAsync(CraftsmanAccountDTO craftmanAccount)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (userId == null)
        //    {
        //        return NotFound("This Token Is Not Found : ");
        //    }


        //    var result = await _crafsmenRepository.UpdateCraftsmanAccountAsync(userId, craftmanAccount);

        //    if (!result.IsUpdated)
        //    {
        //        return BadRequest(result.Message);
        //    } 
            
        //    return Ok(result);




        //}




        [HttpGet("GetPosts")]
        public IActionResult displayPosts() 
        {
            List<Post> posts = postRepo.GetAll();
            if (posts != null)
            {
                return Ok(posts);
            }
            else
                return BadRequest(new { message = "no posted yet" });
           
        }
      
        // 
        [HttpGet("CraftsNames")]
        public async Task<List<string>> GetCraftsNamesAsync()
        {
            return  await _craftRepository.GetAllCraftsNamesAsync();
        }


     


        [HttpGet("CraftsmenOfAcraft")]
        public async Task<ActionResult<List<CraftsmanDTO>>> GetCraftsmenOfACraft(string craftName)
        {
            if (!Enum.TryParse<CraftName>(craftName, out var craftNameAsEnum))
            {
                return BadRequest("Invalid craftName");
            }

            var craftsmen = await _craftRepository.GetCraftsmenOfACraft(craftNameAsEnum);
            return craftsmen;     
        }




        [HttpPost("AddPostToGallary")]
       public async Task<Post> AddPostToGallary([FromBody]PostDTO post) 
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _crafsmenRepository.AddPostToGallaryAsync(userId, post);

        }
      

    }
}
