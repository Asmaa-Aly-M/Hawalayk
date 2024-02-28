using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
