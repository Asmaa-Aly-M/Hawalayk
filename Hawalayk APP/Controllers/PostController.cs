using Hawalayk_APP.Attributes;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ICraftsmenRepository _craftsmanRepository;
        private readonly ICraftRepository _craftRepository;
        public PostController(IPostRepository postRepository, ICraftsmenRepository craftsmanRepository, ICraftRepository craftRepository)
        {
            _postRepository = postRepository;
            _craftsmanRepository = craftsmanRepository;
            _craftRepository = craftRepository;
        }


        [HttpPost("CreatePost")]
        public async Task<IActionResult> post([FromForm] PostDTO post) /////////////////////////Test
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId==null)
            {
                return BadRequest("token=null.");
            }
            await _postRepository.Create(userId, post);
            return Ok("The post created successfully");
        }

        [HttpGet("GetCraftGallary/{craftName}")]
        public async Task<IActionResult> GetGallary(string craftName)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var gallary = await _postRepository.GetGrafGallary(userId, craftName);
            if (gallary != null)
            {
                return Ok(gallary);
            }
            else
                return NotFound(new { message = "no posts yet" });

        }

        [ServiceFilter(typeof(BlockingFilter))]
        [BlockCheck("craftsmanId")]
        [HttpGet("GetCraftsmanPortfolio/{craftsmanId}")]
        public async Task<IActionResult> GetCraftsmanPortfolio(string craftsmanId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var Portfolio = await _postRepository.GetGraftsmanPortfolio(userId, craftsmanId);
            if (Portfolio != null)
            {
                return Ok(Portfolio);
            }
            else
                return NotFound(new { message = "no posts yet" });

        }
        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromForm] PostUpdatedDTO post)
        {
            int raw = await _postRepository.Update(post.Id, post);

            if (raw < 0)
            {
                return BadRequest("There is No Post To Update");
            }
            else if (raw > 0) return Ok("the post updated Successfully");
            else return BadRequest("issue happend while updating :");

        }
        [HttpDelete("DeletePost/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            Post oldpost = await _postRepository.GetById(id);
            if (oldpost == null)
            {
                return NotFound("There is no post to Delete");
            }
            await _postRepository.Delete(id);
            return Ok("The post Deleted successfully");
        }


        [HttpGet("GetMyCraftGallary")]
        public async Task<IActionResult> GetGallary()
        {
            //string craftsmanId
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var craftsman = await _craftsmanRepository.GetById(userId);
            var craftNameEnumValue = craftsman.Craft.Name;
            var craftName = await _craftRepository.GetCraftNameInArabicByEnumValue(craftNameEnumValue);

            var gallary = await _postRepository.GetGrafGallary(userId, craftName);
            if (gallary != null)
            {
                return Ok(gallary);
            }
            else
                return NotFound(new { message = "no posts yet" });

        }
    }
}
