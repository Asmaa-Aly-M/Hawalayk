﻿using Hawalayk_APP.DataTransferObject;
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
        private readonly ICraftsmenRepository craftsmanrepo;
        private readonly ICraftRepository craftRepository;
        public PostController(IPostRepository _postrepository, ICraftsmenRepository _craftsmanrepo, ICraftRepository _craftRepository)
        {
            postrepository = _postrepository;
            craftsmanrepo = _craftsmanrepo;
            craftRepository = _craftRepository;
        }


        [HttpPost("CreatePost")]
        public async Task<IActionResult> post([FromForm] PostDTO post) /////////////////////////Test
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await postrepository.Create(userId, post);
            return Ok("The post created successfully");
        }

        [HttpGet("CraftsGallary/{craftName}")]
        public async Task<IActionResult> getGallary(string craftName)
        {
            var gallary = await postrepository.GetGrafGallary(craftName);
            if (gallary != null)
            {
                return Ok(gallary);
            }
            else
                return NotFound(new { message = "no posts yet" });

        }


        [HttpGet("Portfolio/{craftsmanId}")]
        public async Task<IActionResult> getPortfolio(string craftsmanId)
        {
            //string craftsmanId
            // var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var Portfolio = await postrepository.GetGraftsmanPortfolio(craftsmanId);
            if (Portfolio != null)
            {
                return Ok(Portfolio);
            }
            else
                return NotFound(new { message = "no posts yet" });

        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] PostUpdatedDTO post)
        {
            int raw = await postrepository.Update(post.Id, post);

            if (raw < 0)
            {
                return BadRequest("There is No Post To Update");
            }
            else if (raw > 0) return Ok("the post updated Successfully");
            else return BadRequest("issue happend while updating :");

        }
        [HttpDelete("DeletePost")]
        public async Task<IActionResult> Delete(int id)
        {
            Post oldpost = await postrepository.GetById(id);
            if (oldpost == null)
            {
                return NotFound("There is no post to Delete");
            }
            await postrepository.Delete(id);
            return Ok("The post Deleted successfully");
        }


        [HttpGet("MyCraftsGallary")]
        public async Task<IActionResult> getGallary()
        {
            //string craftsmanId
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var craftsman = await craftsmanrepo.GetById(userId);
            var craftNameEnumValue = craftsman.Craft.Name;
            var craftName = await craftRepository.GetCraftNameInArabicByEnumValue(craftNameEnumValue);

            var gallary = await postrepository.GetGrafGallary(craftName);
            if (gallary != null)
            {
                return Ok(gallary);
            }
            else
                return NotFound(new { message = "no posts yet" });

        }
    }
}
