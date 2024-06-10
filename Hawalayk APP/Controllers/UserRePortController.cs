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
    public class UserRePortController : ControllerBase
    {
        private readonly IUserReportRepository _userReportRepository;
        public UserRePortController(IUserReportRepository userReportRepository)
        {
            _userReportRepository = userReportRepository;
        }

        [HttpPost("Write a Report")]
        public async Task<IActionResult> writeReport(UserReportDTO userReport)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var newReport=await _userReportRepository.Create(userId, userReport);
            return Ok(newReport);



        }

    }
}
