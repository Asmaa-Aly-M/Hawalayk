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
        private readonly IPostRepository postrepository;
        public PostController(IPostRepository _postrepository)
        {
            postrepository = _postrepository;
        }


        [HttpPost("CreatePost")]
        public IActionResult post([FromForm] PostDTO post) /////////////////////////Test
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            postrepository.Create(userId, post);
            return Ok("The post created successfully");
        }

        [HttpGet("CraftsGallary/{craftName}")]
        public IActionResult getGallary(string craftName)
        {
            var gallary = postrepository.GetGrafGallary(craftName);
            if (gallary != null)
            {
                return Ok(gallary);
            }
            else
                return NotFound(new { message = "no posts yet" });

        }


        [HttpGet("Portfolio/{craftsmanId}")]
        public IActionResult getPortfolio(string craftsmanId)
        {
            //string craftsmanId
            // var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var Portfolio = postrepository.GetGraftsmanPortfolio(craftsmanId);
            if (Portfolio != null)
            {
                return Ok(Portfolio);
            }
            else
                return NotFound(new { message = "no posts yet" });

        }
        [HttpPut("Update")]
        public IActionResult Update(int id, Post post)
        {
            Post oldpost = postrepository.GetById(id);
            postrepository.Update(id, oldpost);
            return Ok();

        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            Post oldpost = postrepository.GetById(id);
            if (oldpost == null)
            {
                return NotFound();
            }
            postrepository.Delete(id);
            return NoContent();
        }
    }
}
