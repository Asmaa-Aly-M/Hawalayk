using Hawalayk_APP.Attributes;
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

        [ServiceFilter(typeof(BlockingFilter))]
        [BlockCheck("reportedUserId")]
        [HttpPost("WriteReport/{reportedUserId}")]
        public async Task<IActionResult> WriteReport(string reportedUserId, UserReportDTO userReport)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var newReport=await _userReportRepository.Create(userId, reportedUserId, userReport);
            return Ok(newReport);



        }

    }
}
