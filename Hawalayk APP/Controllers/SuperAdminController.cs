using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        IAdminRepository adminRepo;
        public SuperAdminController(IAdminRepository adminRepository ) { 

            adminRepo = adminRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var admins = adminRepo.GetAll();
            return Ok(admins);
        }
        //[HttpPost]
        //public IActionResult AddAdmin(Admin admin)
        //{

        //}
        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{

        //}
    }
}
