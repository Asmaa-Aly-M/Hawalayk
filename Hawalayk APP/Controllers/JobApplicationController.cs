using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        public JobApplicationController(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        [HttpGet("GetAcceptedJobApplicationByServiceRequestId")]
        public async Task<IActionResult> GetAcceptedJobApplicationByServiceRequest(int serviceRequestId)
        {
            var job = await _jobApplicationRepository.GetJobApplicationAcceptedByServiceRequest(serviceRequestId);

            if (job == null)
            {
                return NotFound($"No accepted job application found for service request ID {serviceRequestId}");
            }

            return Ok(job);
        }

        [HttpGet("GetPendingJobApplicationByServiceRequestId")]
        public async Task<IActionResult> GetJobApplicationsPendingByServiceRequest(int serviceRequestId)
        {
            var jobs = await _jobApplicationRepository.GetJobApplicationsPendingByServiceRequest(serviceRequestId);
            if (jobs == null)
            {
                return NotFound($"No accepted job application found for service request ID {serviceRequestId}");
            }
            return Ok(jobs);
        }

    }
}
