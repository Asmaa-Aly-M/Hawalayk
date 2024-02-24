﻿using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CraftsmenController : ControllerBase
    {
        IPostRepository postRepo;
        private readonly ICraftRepository _crafRepository;
        private readonly ICraftsmenRepository _crafsmenRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CraftsmenController(IPostRepository _postRepo, ICraftRepository crafRepository, ICraftsmenRepository crafsmenRepository, UserManager<ApplicationUser> userManager) 
        {
            postRepo= _postRepo;
            _crafRepository= crafRepository;
            _userManager= userManager;
            _crafsmenRepository= crafsmenRepository;
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
      
        // 
        [HttpGet("CraftsNames")]
        public async Task<List<string>> GetCraftsNamesAsync()
        {
            return  await _crafRepository.GetAllCraftsNamesAsync();
        }

    }
}
