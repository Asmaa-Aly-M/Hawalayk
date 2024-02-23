using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CraftsmenController : ControllerBase
    {
        PostRepository postRepo;
        public CraftsmenController(PostRepository _postRepo) 
        {
            postRepo= _postRepo;
        }

        [HttpPost]
        public IActionResult addPost (Post newPost )
        {
            postRepo.Create(newPost);
            return Ok(new { message = "post is added" });
        }

        [HttpGet]
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

    }
}
