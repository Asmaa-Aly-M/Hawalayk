using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        IServiceRequestRepository serviceRequestRepo;
        public ServiceRequestController(IServiceRequestRepository _serviceRequestRepo)
        {
           serviceRequestRepo = _serviceRequestRepo;
        }

        [HttpDelete]
        public IActionResult cancleService(int serviceId) 
        {
            serviceRequestRepo.Delete(serviceId);
            return Ok(new {message= "Service is canceled" });
        }
    }
}
