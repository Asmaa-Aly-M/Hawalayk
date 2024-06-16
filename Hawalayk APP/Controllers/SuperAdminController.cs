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
        private readonly IAdminRepository _adminRepository;
        public SuperAdminController(IAdminRepository _adminRepositorysitory ) 
        { 
            _adminRepository = _adminRepositorysitory;
        }

        [HttpGet("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _adminRepository.GetAll();
            return Ok(admins);
        }

        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin(Admin admin)
        {
            await _adminRepository.Create(admin);
            return Ok();
        }

        [HttpDelete("DeleteAdmin")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            var existingAmin = await _adminRepository.GetById(id);
            if (existingAmin == null)
            {
                return NotFound();
            }
            await _adminRepository.Delete(id);
            return NoContent();
        }
    }
}
