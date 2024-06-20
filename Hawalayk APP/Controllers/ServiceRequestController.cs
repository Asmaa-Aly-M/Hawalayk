using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Hawalayk_APP.System_Hub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly ICraftsmenRepository _craftsmenRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _notificationHub;

        private readonly ICraftRepository _craftRepository;


        public ServiceRequestController(ICraftRepository craftRepository, ApplicationDbContext context, IServiceRequestRepository serviceRequestRepository, IHubContext<NotificationHub> hubContext, IJobApplicationRepository jobApplicationRepository, ICraftsmenRepository craftsmenRepository)
        {
            _serviceRequestRepository = serviceRequestRepository;
            _notificationHub = hubContext;
            _craftRepository = craftRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _context = context;
            _craftsmenRepository = craftsmenRepository;
        }


        [HttpPost("CreateServiceRequest/{craftName}")]//
        public async Task<IActionResult> CreateServiceRequest(string craftName,[FromForm] ServiceRequestDTO ServiceRequest)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("No token was sent");
            }

            var serviceRequestId = await _serviceRequestRepository.CreateAsync(craftName, userId, ServiceRequest);
            if (serviceRequestId== 0) 
            {
                return BadRequest("No craftsmen in this craft");
            }

            if (serviceRequestId != -1)
            {
                var serviceSend = await _serviceRequestRepository.GetServiceRequestSend(serviceRequestId);
                _notificationHub.Clients.Group(craftName).SendAsync("ReceiveNotification", serviceSend);//// signalR سطر ال 
                return Ok("Request sent successfully");

            }

            return BadRequest("Please Try again");

        }




        [HttpDelete("CancelServiceRequest/{serviceId}")]
        public async Task<IActionResult> CancelServiceRequest(int serviceId) ////محتاجة مراجعة؟؟؟
        {
            await _serviceRequestRepository.Delete(serviceId);
            return Ok(new { message = "Service is canceled" });
        }
        //http://localhost:5153/api/ServiceRequest/applyToRequest

        [HttpPost("ApplyToServiceRequest/{serviceId}")]
        public async Task<IActionResult> ApplyToServiceRequest(int serviceID,JobApplicationDTO replay)
        {
            //   var customerID = (await _serviceRequestRepository.GetById(requestId)).Customer.Id;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("invalid token");
            }
            var jobApplicationId = await _jobApplicationRepository.Create(userId, serviceID, replay);
            if (jobApplicationId != -1)
            {
                var jobApplicationSend = await _jobApplicationRepository.GetJpbApplicationSend(jobApplicationId);
                _notificationHub.Clients.User(replay.customerId).SendAsync("ApplyNotification", jobApplicationSend);
                return Ok("Sent successfully");
            }
            return BadRequest("an error occured ");

        }

        [HttpPost("AcceptJobApplication/{applicationId}")]
        public async Task<IActionResult> AcceptJobApplication(int applicationId)
        {
            var aJobapplication= await _jobApplicationRepository.GetById(applicationId);
            aJobapplication.ResponseStatus = ResponseStatus.Accepted;
            //get serviceRequest id
            var aServiceRequestId = aJobapplication.ServiceRequestId;
            ///get All Jopapplicatoin For this ServicceRequest
            var jobsapplications = await _serviceRequestRepository.getAllJopapplicatoinForAServicceRequest(aServiceRequestId);
            foreach (var application in jobsapplications)
            {
                if (application.Id != applicationId)
                {
                    application.ResponseStatus = ResponseStatus.Rejected;
                    /*var craftmanId = (await _jobApplicationRepository.GetById(applicationId)).Craftsman.Id;
                    _notificationHub.Clients.User(craftmanId).SendAsync("RejectApplyRequest");*/
                }
            }
            await _context.SaveChangesAsync();
            var craftmanID = (await _jobApplicationRepository.GetById(applicationId)).Craftsman.Id;
            _notificationHub.Clients.User(craftmanID).SendAsync("AcceptApplyRequest");
            return Ok("accept");
        }

        [HttpPost("RejectJobApplication/{applicationId}")]
        public async Task<IActionResult> RejectJobApplication(int applicationId)
        {
            (await _jobApplicationRepository.GetById(applicationId)).ResponseStatus = ResponseStatus.Rejected;
            await _context.SaveChangesAsync();
            var craftmanID = (await _jobApplicationRepository.GetById(applicationId)).Craftsman.Id;
            _notificationHub.Clients.User(craftmanID).SendAsync("RejectApplyRequest");
            return Ok("this service not available");
        }

        [HttpGet("GetNumberOfAllServiceRequests")]
        public async Task<IActionResult> GetNumberOfAllServiceRequests()
        {
            int counter = await _serviceRequestRepository.countService();
            return Ok(counter);
        }

        /* مدش عرف هى معموة ليه هنعملها comment لغاية لما حد يعرف
          [HttpGet("Get All Request in App")]
        public async Task<IActionResult> getAllRequest()
        {
            List<ServiceRequest> allRequest = await _serviceRequestRepository.GetAll();
            return Ok(allRequest);
        }
        */


        [HttpGet("GetServiceRequestsWithNoAcceptedJobApplicationsForCustomer")]
        public async Task<IActionResult> GetServiceRequestsWithNoAcceptedJobApplicationsForCustomer()
        {
         
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var requests = await _serviceRequestRepository.GetServiceRequestsNeedToReplayByCraftsmenForCustomer(userId);

            return Ok(requests);

        }



        [HttpGet("GetServiceRequestsWithAcceptedJobApplicationForCustomer")]
        public async Task<IActionResult> GetServiceRequestsWithAcceptedJobApplicationForCustomer()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("invalid token");
            }
            var requests = await _serviceRequestRepository.GetServiceRequestsAcceptedCraftsmenForCustomer(userId);
            if (requests == null)
            {
                return NotFound("No accepted service requests found for the customer.");
            }


            return Ok(requests);

        }

       

    }
}
