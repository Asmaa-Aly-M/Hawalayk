using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public IActionResult reportAPP(AppReport newReport)
        {
            reportRepo.Create(newReport);
            return Ok();///// محتاجين ترجع حاجة معينة؟
        }
    }
}
