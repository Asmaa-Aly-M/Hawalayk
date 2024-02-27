using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult writeReport(UserReport Report) ////////// مراجعة
        {
           userReportRepo.Create(Report);
            return Ok();
        }

    }
}
