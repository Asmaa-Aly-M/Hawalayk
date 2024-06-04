using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        IJobApplicationRepository jobApplicationRepo;
        public JobApplicationController(IJobApplicationRepository _jobApplicationRepo)
        {
            jobApplicationRepo = _jobApplicationRepo;
        }

        [HttpGet("Get Accepted JobApplication By ServiceRequestId")]
        public async Task<IActionResult> GetJobApplicationAcceptedByServiceRequest(int serviceRequestId)
        {
            var job = await jobApplicationRepo.GetJobApplicationAcceptedByServiceRequest(serviceRequestId);
            return Ok(job);
        }

        [HttpGet("Get Pending JobApplication By ServiceRequestId")]
        public async Task<IActionResult> GetJobApplicationsPendingByServiceRequest(int serviceRequestId)
        {
            var jobs = await jobApplicationRepo.GetJobApplicationsPendingByServiceRequest(serviceRequestId);
            return Ok(jobs);
        }

    }
}
