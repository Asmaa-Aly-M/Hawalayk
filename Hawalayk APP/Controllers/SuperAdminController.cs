using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hawalayk_APP.Controllers
//الكنترولر كامل محتاج تيست علشان عندي مشكلة في الداتا بيز
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
        [HttpPost]
        public IActionResult AddAdmin(Admin admin)
        {
            adminRepo.Create(admin);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteAdmin(string id)
        {
            var existingAmin = adminRepo.GetById(id);
            if (existingAmin == null)
            {
                return NotFound();
            }
            adminRepo.Delete(id);
            return NoContent();
        }
    }
}
