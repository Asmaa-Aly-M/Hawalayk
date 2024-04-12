using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Hawalayk_APP.System_Hub;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Twilio.TwiML.Messaging;

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

        [HttpPost]
        public IActionResult createRequest(string craftName,ServiceRequestDTO ServiceRequest) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            serviceRequestRepo.Create(userId, ServiceRequest);
            hubContext.Clients.Group(craftName).SendAsync("ReceiveNotification", ServiceRequest);//// signalR سطر ال 
            return Ok("Request sent successfully");
        }


        [HttpDelete]
        public IActionResult cancleService(int serviceId) ////محتاجة مراجعة؟؟؟
        {
            serviceRequestRepo.Delete(serviceId);
            return Ok(new {message= "Service is canceled" });
        }


        /*[HttpPost]
        public IActionResult applyToRequest(int requestId,JobApplicationDTO replay)
        {
            var customerID=serviceRequestRepo.GetById(requestId).Customer.Id;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            jobApplicationRepo.Create(userId, replay);
            hubContext.Clients.User(customerID).SendAsync("ApplyNotification", replay);
            return Ok("successfully");
        }*/
    }
}
