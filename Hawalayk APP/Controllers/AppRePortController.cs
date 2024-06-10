using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppRePortController : ControllerBase
    {
        private readonly IAppReportRepository _reportRepository;

        public AppRePortController(IAppReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        [HttpPost("ReportApp")]
        public async Task<IActionResult> reportAPP([FromBody] AppReportDTO newReport)
        {
            var reporterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (reporterId==null)
            {
                return Unauthorized("User ID is missing");
            }
            var report= await _reportRepository.Create(reporterId, newReport);
            //return Ok(new { message = "Done" });///// محتاجين ترجع حاجة معينة؟
            return Ok(report);
        }

    }
}
