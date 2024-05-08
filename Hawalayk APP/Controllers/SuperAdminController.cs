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
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await adminRepo.GetAll();
            return Ok(admins);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(Admin admin)
        {
            await adminRepo.Create(admin);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            var existingAmin = await adminRepo.GetById(id);
            if (existingAmin == null)
            {
                return NotFound();
            }
            await adminRepo.Delete(id);
            return NoContent();
        }
    }
}
