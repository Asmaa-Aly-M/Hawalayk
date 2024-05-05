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
        IAppReportRepository reportRepo;

        public AppRePortController(IAppReportRepository _reportRepo)
        {
            reportRepo = _reportRepo;
        }
        [HttpPost("ReportApp")]
        public async Task<IActionResult> reportAPP([FromBody] AppReportDTO newReport)
        {
            var reporterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await reportRepo.Create(reporterId, newReport);
            return Ok(new { message = "Done" });///// محتاجين ترجع حاجة معينة؟
        }

    }
}
