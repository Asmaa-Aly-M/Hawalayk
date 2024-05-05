using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Hawalayk_APP.System_Hub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestRepository serviceRequestRepo;
        private readonly IJobApplicationRepository jobApplicationRepo;
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<Notification> _hubContext;
        private readonly ICraftRepository _craftRepository;


        public ServiceRequestController(ICraftRepository craftRepository, ApplicationDbContext context, IServiceRequestRepository _serviceRequestRepo, IHubContext<Notification> hubContext, IJobApplicationRepository _jobApplicationRepo)
        {
            serviceRequestRepo = _serviceRequestRepo;
            _hubContext = hubContext;
            _craftRepository = craftRepository;
            jobApplicationRepo = _jobApplicationRepo;
            _context = context;

        }

        [HttpPost("CreateRequest")]//
        public async Task<IActionResult> createRequest([FromForm] ServiceRequestDTO ServiceRequest)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await serviceRequestRepo.CreateAsync(userId, ServiceRequest);

            //
            var CraftsmentOfACraft = await _craftRepository.GetCraftsmenOfACraft(ServiceRequest.craftName);

            foreach (var craftsmen in CraftsmentOfACraft)
            {
                // Send notification to each painter
                //  await _hubContext.Clients.User(craftsmen.Id.ToString()).SendAsync("ReceiveNotification", "New service request created");

                await _hubContext.Clients.User(craftsmen.Id.ToString()).SendAsync("ReceiveNotification", "New service request available");
            }

            //   _hubContext.Clients.Group(ServiceRequest.craftName).SendAsync("ReceiveNotification", ServiceRequest);//// signalR سطر ال 
            return Ok("Request sent successfully");
        }


        [HttpDelete]
        public async Task<IActionResult> cancleService(int serviceId) ////محتاجة مراجعة؟؟؟
        {
            await serviceRequestRepo.Delete(serviceId);
            return Ok(new { message = "Service is canceled" });
        }


        [HttpPost("applyToRequest")]
        public async Task<IActionResult> applyToRequest(int requestId, JobApplicationDTO replay)
        {
            var customerID = (await serviceRequestRepo.GetById(requestId)).Customer.Id;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await jobApplicationRepo.Create(userId, replay);
            _hubContext.Clients.User(customerID).SendAsync("ApplyNotification", replay);
            return Ok("Sent successfully");
        }

        [HttpPost("acceptApply")]
        public async Task<IActionResult> acceptApply(int repplyId)
        {
            (await jobApplicationRepo.GetById(repplyId)).ResponseStatus = ResponseStatus.Accepted;
            var craftmanID = (await jobApplicationRepo.GetById(repplyId)).Craftsman.Id;
            _hubContext.Clients.User(craftmanID).SendAsync("AcceptApplyRequest");
            return Ok("accept");
        }

        [HttpPost("rejectApply")]
        public async Task<IActionResult> rejectApply(int repplyId)
        {
            (await jobApplicationRepo.GetById(repplyId)).ResponseStatus = ResponseStatus.Rejected;
            var craftmanID = (await jobApplicationRepo.GetById(repplyId)).Craftsman.Id;
            _hubContext.Clients.User(craftmanID).SendAsync("RejectApplyRequest");
            return Ok("this service not available");
        }

        [HttpGet]
        public async Task<IActionResult> numberOfServiceRequest()
        {
            int counter = await serviceRequestRepo.countService();
            return Ok(counter);
        }

        [HttpGet("getAllRequest")]
        public async Task<IActionResult> getAllRequest()
        {
            List<ServiceRequest> allRequest = await serviceRequestRepo.GetAll();
            return Ok(allRequest);
        }

    }
}
