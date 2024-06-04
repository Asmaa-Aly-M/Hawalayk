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
        private readonly ICraftsmenRepository CraftsmenRepo;
        private readonly IJobApplicationRepository jobApplicationRepo;
        private readonly ApplicationDbContext _context;
        IHubContext<NotificationHub> _notificationHub;

        private readonly ICraftRepository _craftRepository;


        public ServiceRequestController(ICraftRepository craftRepository, ApplicationDbContext context, IServiceRequestRepository _serviceRequestRepo, IHubContext<NotificationHub> hubContext, IJobApplicationRepository _jobApplicationRepo, ICraftsmenRepository _CraftsmenRepo)
        {
            serviceRequestRepo = _serviceRequestRepo;
            _notificationHub = hubContext;
            _craftRepository = craftRepository;
            jobApplicationRepo = _jobApplicationRepo;
            _context = context;
            CraftsmenRepo = _CraftsmenRepo;
        }


        [HttpPost("CreateRequest")]//
        public async Task<IActionResult> createRequest([FromForm] ServiceRequestDTO ServiceRequest)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("No token was sent");
            }

            var serviceRequestId = await serviceRequestRepo.CreateAsync(userId, ServiceRequest);

            if (serviceRequestId != -1)
            {
                var serviceSend = await serviceRequestRepo.GetServiceRequestSend(serviceRequestId);
                _notificationHub.Clients.Group(ServiceRequest.craftName).SendAsync("ReceiveNotification", serviceSend);//// signalR سطر ال 
                return Ok("Request sent successfully");

            }

            return BadRequest("Please Try again");

        }




        [HttpDelete]
        public async Task<IActionResult> cancleService(int serviceId) ////محتاجة مراجعة؟؟؟
        {
            await serviceRequestRepo.Delete(serviceId);
            return Ok(new { message = "Service is canceled" });
        }
        //http://localhost:5153/api/ServiceRequest/applyToRequest

        [HttpPost("applyToRequest")]
        public async Task<IActionResult> applyToRequest(JobApplicationDTO replay)
        {
            //   var customerID = (await serviceRequestRepo.GetById(requestId)).Customer.Id;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("invalid token");
            }
            var jobApplicationId = await jobApplicationRepo.Create(userId, replay);
            if (jobApplicationId != -1)
            {
                var jobApplicationSend = await jobApplicationRepo.GetJpbApplicationSend(jobApplicationId);
                _notificationHub.Clients.User(replay.customerId).SendAsync("ApplyNotification", jobApplicationSend);
                return Ok("Sent successfully");
            }
            return BadRequest("an error occured ");

        }

        [HttpPost("acceptApply")]
        public async Task<IActionResult> acceptApply(int repplyId)
        {
            (await jobApplicationRepo.GetById(repplyId)).ResponseStatus = ResponseStatus.Accepted;
            var craftmanID = (await jobApplicationRepo.GetById(repplyId)).Craftsman.Id;
            _notificationHub.Clients.User(craftmanID).SendAsync("AcceptApplyRequest");
            return Ok("accept");
        }

        [HttpPost("rejectApply")]
        public async Task<IActionResult> rejectApply(int repplyId)
        {
            (await jobApplicationRepo.GetById(repplyId)).ResponseStatus = ResponseStatus.Rejected;
            var craftmanID = (await jobApplicationRepo.GetById(repplyId)).Craftsman.Id;
            _notificationHub.Clients.User(craftmanID).SendAsync("RejectApplyRequest");
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


        [HttpGet("Get Service Requests Needed To Replay By craftsmen for Craftsman")]
        public async Task<IActionResult> RequestsByCraftName( )
        {
            //var craft_name = await _craftRepository.GetEnumValueOfACraftByArabicDesCription(craftName);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var craftsman = await CraftsmenRepo.GetById(userId);
            var craftName = craftsman.Craft.Name;
            var requests = await serviceRequestRepo.GetServiceRequestsNeedToReplayByCraftsmen(craftName);

            return Ok(requests);

        }

        [HttpGet("Get Accepted Service Requests By Customer for Craftsman")]
        public async Task<IActionResult> AcceptedRequestServiceForCrafsman()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var requests = await serviceRequestRepo.GetAcceptedServiceRequestsFromCustomersByACraftsman(userId);

            return Ok(requests);

        }


        [HttpGet("Get Service Requests Needed To Replay By craftsmen for Customer")]
        public async Task<IActionResult> ServiceRequestsNeededToReplayByCraftsmen()
        {
         
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var requests = await serviceRequestRepo.GetServiceRequestsNeedToReplayByCraftsmenForCustomer(userId);

            return Ok(requests);

        }



        [HttpGet("Get Accepted Service Requests for Customer")]
        public async Task<IActionResult> AcceptedRequestServiceForCustomer()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var requests = await serviceRequestRepo.GetServiceRequestsAcceptedCraftsmenForCustomer(userId);

            return Ok(requests);

        }

       

    }
}
