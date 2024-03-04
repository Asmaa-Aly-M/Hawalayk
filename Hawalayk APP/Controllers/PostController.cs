using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostRepository postrepository;
        PostController(IPostRepository _postrepository) 
        {
            postrepository= _postrepository;
        }
        [HttpPost]
        public IActionResult post(PostDTO post) //////////////////Test
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            postrepository.Create(userId, post);
            return Ok();
        }

        [HttpGet]
        public IActionResult getGallary(int craftId)
        {
            var gallary = postrepository.GetGrafGallary(craftId);
            if (gallary != null)
            {
                return Ok(gallary);
            }
            else
                return NotFound(new { message = "no posts yet" });

        }

        [Route("Portfolio")]
        [HttpGet]
        public IActionResult getPortfolio(string craftsmanId)
        {
            var Portfolio = postrepository.GetGraftsmanPortfolio(craftsmanId);
            if (Portfolio != null)
            {
                return Ok(Portfolio);
            }
            else
                return NotFound(new { message = "no posts yet" });

        }
        [HttpPut]
        public IActionResult Update(int id,Post post)
        {
            Post oldpost = postrepository.GetById(id);
            postrepository.Update(id, oldpost);
            return Ok();
            
        }
        [HttpDelete]
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
