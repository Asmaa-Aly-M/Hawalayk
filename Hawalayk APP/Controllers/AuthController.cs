using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult>RegisterCustomerAsync([FromBody] RegisterCustomerModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.RegisterCustomerAsync(model);
            if(!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("RegisterCraftsman")]
        public async Task<IActionResult> RegisterCraftsmanAsync([FromBody] RegisterCraftsmanModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.RegisterCraftsmanAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] string phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.ForgotPasswordAsync(phoneNumber);

            if (!result.ActionSucceeded)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.ResetPasswordAsync(model);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult>GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.GetTokenAsync(model);
            if(!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTPAsync([FromBody] VerifyOTPModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.VerifyOTPAsync(model.PhoneNumber, model.OTP);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
        [Authorize]
        [HttpDelete("DeleteMyAccount")]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("This Token Not Valid : ");
            }
            var result =  await _authService.DeleteUserAsync(userId);
            if(result.isDeleted == false) 
            {
                return BadRequest(result.Message);
            }
            return Ok(result);


        }

        //[HttpPut("UpdateAccount")]
      
    }
}
