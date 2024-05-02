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
        IServiceRequestRepository serviceRequestRepo;
        IJobApplicationRepository jobApplicationRepo;
        IHubContext<Notification> hubContext;
        public ServiceRequestController(IServiceRequestRepository _serviceRequestRepo, IHubContext<Notification> _hubContext, IJobApplicationRepository _jobApplicationRepo)
        {
            serviceRequestRepo = _serviceRequestRepo;
            hubContext = _hubContext;
            jobApplicationRepo = _jobApplicationRepo;
        }

        [HttpPost("CreateRequest")]//
        public IActionResult createRequest(ServiceRequestDTO ServiceRequest)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            serviceRequestRepo.Create(userId, ServiceRequest);


            hubContext.Clients.Group(ServiceRequest.craftName).SendAsync("ReceiveNotification", ServiceRequest);//// signalR سطر ال 
            return Ok("Request sent successfully");
        }


        [HttpDelete]
        public IActionResult cancleService(int serviceId) ////محتاجة مراجعة؟؟؟
        {
            serviceRequestRepo.Delete(serviceId);
            return Ok(new { message = "Service is canceled" });
        }


        [HttpPost("applyToRequest")]
        public IActionResult applyToRequest(int requestId, JobApplicationDTO replay)
        {
            var customerID = serviceRequestRepo.GetById(requestId).Customer.Id;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            jobApplicationRepo.Create(userId, replay);
            hubContext.Clients.User(customerID).SendAsync("ApplyNotification", replay);
            return Ok("Sent successfully");
        }

        [HttpPost("acceptApply")]
        public IActionResult acceptApply(int repplyId)
        {
            jobApplicationRepo.GetById(repplyId).ResponseStatus = ResponseStatus.Accepted;
            var craftmanID = jobApplicationRepo.GetById(repplyId).Craftsman.Id;
            hubContext.Clients.User(craftmanID).SendAsync("AcceptApplyRequest");
            return Ok("accept");
        }

        [HttpPost("rejectApply")]
        public IActionResult rejectApply(int repplyId)
        {
            jobApplicationRepo.GetById(repplyId).ResponseStatus = ResponseStatus.Rejected;
            var craftmanID = jobApplicationRepo.GetById(repplyId).Craftsman.Id;
            hubContext.Clients.User(craftmanID).SendAsync("RejectApplyRequest");
            return Ok("this service not available");
        }

        [HttpGet]
        public IActionResult numberOfServiceRequest()
        {
            int counter = serviceRequestRepo.countService();
            return Ok(counter);
        }

        [HttpGet("getAllRequest")]
        public IActionResult getAllRequest()
        {
            List<ServiceRequest> allRequest = serviceRequestRepo.GetAll();
            return Ok(allRequest);
        }

    }
}
