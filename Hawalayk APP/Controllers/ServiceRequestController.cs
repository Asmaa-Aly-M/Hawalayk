////using Hawalayk_APP.Context;
////using Hawalayk_APP.DataTransferObject;
////using Hawalayk_APP.Enums;
////using Hawalayk_APP.Models;
////using Hawalayk_APP.Services;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.SignalR;
////using System.Security.Claims;

////namespace Hawalayk_APP.Controllers
////{
////    [Route("api/[controller]")]
////    [ApiController]
////    public class ServiceRequestController : ControllerBase
////    {
////        private readonly IServiceRequestRepository serviceRequestRepo;
////        private readonly IJobApplicationRepository jobApplicationRepo;
////        private readonly ApplicationDbContext _context;
////        private readonly NotificationHub _notificationHub;
////        private readonly ICraftRepository _craftRepository;


////        public ServiceRequestController(ICraftRepository craftRepository, ApplicationDbContext context, IServiceRequestRepository _serviceRequestRepo, NotificationHub hubContext, IJobApplicationRepository _jobApplicationRepo)
////        {
////            serviceRequestRepo = _serviceRequestRepo;
////            _notificationHub = hubContext;
////            _craftRepository = craftRepository;
////            jobApplicationRepo = _jobApplicationRepo;
////            _context = context;

////        }

////        [HttpPost("CreateRequest")]//
////        public async Task<IActionResult> createRequest([FromForm] ServiceRequestDTO ServiceRequest)
////        {

////            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
////            await serviceRequestRepo.CreateAsync(userId, ServiceRequest);
////            await _notificationHub.SendServiceRequestNotification(ServiceRequest);

////            //  return CreatedAtRoute("GetServiceRequest", new { id = request.Id }, request);


//<<<<<<< HEAD
////            //
////            //var CraftsmentOfACraft = await _craftRepository.GetCraftsmenOfACraft(ServiceRequest.craftName);

////            //foreach (var craftsmen in CraftsmentOfACraft)
////            //{
////            //    // Send notification to each painter
////            //    //  await _hubContext.Clients.User(craftsmen.Id.ToString()).SendAsync("ReceiveNotification", "New service request created");

////            //    await _hubContext.Clients.User(craftsmen.Id.ToString()).SendAsync("ReceiveNotification", "New service request available");
////            //}

////            ////   _hubContext.Clients.Group(ServiceRequest.craftName).SendAsync("ReceiveNotification", ServiceRequest);//// signalR سطر ال 
////            return Ok("Request sent successfully");
////        }


////        [HttpDelete]
////        public IActionResult cancleService(int serviceId) ////محتاجة مراجعة؟؟؟
////        {
////            serviceRequestRepo.Delete(serviceId);
////            return Ok(new { message = "Service is canceled" });
////        }


////        [HttpPost("applyToRequest")]
////        public IActionResult applyToRequest(int requestId, JobApplicationDTO replay)
////        {
////            var customerID = serviceRequestRepo.GetById(requestId).Customer.Id;
////            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
////            jobApplicationRepo.Create(userId, replay);
////            _notificationHub.Clients.User(customerID).SendAsync("ApplyNotification", replay);
////            return Ok("Sent successfully");
////        }

////        [HttpPost("acceptApply")]
////        public IActionResult acceptApply(int repplyId)
////        {
////            jobApplicationRepo.GetById(repplyId).ResponseStatus = ResponseStatus.Accepted;
////            var craftmanID = jobApplicationRepo.GetById(repplyId).Craftsman.Id;
////            _notificationHub.Clients.User(craftmanID).SendAsync("AcceptApplyRequest");
////            return Ok("accept");
////        }

////        [HttpPost("rejectApply")]
////        public IActionResult rejectApply(int repplyId)
////        {
////            jobApplicationRepo.GetById(repplyId).ResponseStatus = ResponseStatus.Rejected;
////            var craftmanID = jobApplicationRepo.GetById(repplyId).Craftsman.Id;
////            _notificationHub.Clients.User(craftmanID).SendAsync("RejectApplyRequest");
////            return Ok("this service not available");
////        }
//=======
//        [HttpDelete]
//        public async Task<IActionResult> cancleService(int serviceId) ////محتاجة مراجعة؟؟؟
//        {
//            await serviceRequestRepo.Delete(serviceId);
//            return Ok(new { message = "Service is canceled" });
//        }


//        [HttpPost("applyToRequest")]
//        public async Task<IActionResult> applyToRequest(int requestId, JobApplicationDTO replay)
//        {
//            var customerID = (await serviceRequestRepo.GetById(requestId)).Customer.Id;
//            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            await jobApplicationRepo.Create(userId, replay);
//            _hubContext.Clients.User(customerID).SendAsync("ApplyNotification", replay);
//            return Ok("Sent successfully");
//        }

//        [HttpPost("acceptApply")]
//        public async Task<IActionResult> acceptApply(int repplyId)
//        {
//            (await jobApplicationRepo.GetById(repplyId)).ResponseStatus = ResponseStatus.Accepted;
//            var craftmanID = (await jobApplicationRepo.GetById(repplyId)).Craftsman.Id;
//            _hubContext.Clients.User(craftmanID).SendAsync("AcceptApplyRequest");
//            return Ok("accept");
//        }

//        [HttpPost("rejectApply")]
//        public async Task<IActionResult> rejectApply(int repplyId)
//        {
//            (await jobApplicationRepo.GetById(repplyId)).ResponseStatus = ResponseStatus.Rejected;
//            var craftmanID = (await jobApplicationRepo.GetById(repplyId)).Craftsman.Id;
//            _hubContext.Clients.User(craftmanID).SendAsync("RejectApplyRequest");
//            return Ok("this service not available");
//        }

//        [HttpGet]
//        public async Task<IActionResult> numberOfServiceRequest()
//        {
//            int counter = await serviceRequestRepo.countService();
//            return Ok(counter);
//        }

//        [HttpGet("getAllRequest")]
//        public async Task<IActionResult> getAllRequest()
//        {
//            List<ServiceRequest> allRequest = await serviceRequestRepo.GetAll();
//            return Ok(allRequest);
//        }
//>>>>>>> d44a62dc11f45570f618f209d5262876bbd75df0

////        [HttpGet]
////        public IActionResult numberOfServiceRequest()
////        {
////            int counter = serviceRequestRepo.countService();
////            return Ok(counter);
////        }

////        [HttpGet("getAllRequest")]
////        public IActionResult getAllRequest()
////        {
////            List<ServiceRequest> allRequest = serviceRequestRepo.GetAll();
////            return Ok(allRequest);
////        }

////    }
////}
