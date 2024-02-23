using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.Rest.Taskrouter.V1.Workspace;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISMSService _smsService;
        public AccountController(ISMSService smsService)
        {
            _smsService = smsService;
        }
        [HttpPost]
        public IActionResult ResetPassword(SendSMSDTO smsDTO)
        {
            var result = _smsService.SendSMS(smsDTO.PhoneNumber,smsDTO.Body);
            if (!String.IsNullOrEmpty(result.ErrorMessage))
            {
                return BadRequest(result.ErrorMessage);

            }
            return Ok(result);
        }
    }
}
