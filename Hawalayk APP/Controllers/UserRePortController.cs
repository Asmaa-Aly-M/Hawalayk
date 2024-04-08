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
        IUserReportRepository userReportRepo;
        public UserRePortController(IUserReportRepository _userReportRepo)
        {
            userReportRepo = _userReportRepo;
        }

        [HttpPost]
        public IActionResult writeReport(UserReportDTO userReport)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            userReportRepo.Create(userId, userReport);
            return Ok();



        }

    }
}
