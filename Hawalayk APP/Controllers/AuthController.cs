using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        public AuthController(IAuthRepository authRepository, IApplicationUserRepository applicationUserRepository)
        {
            _authRepository = authRepository;
            _applicationUserRepository = applicationUserRepository;
        }
        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomerAsync([FromForm] RegisterCustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authRepository.RegisterCustomerAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("RegisterCraftsman")]
        public async Task<IActionResult> RegisterCraftsmanAsync([FromForm] RegisterCraftsmanModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authRepository.RegisterCraftsmanAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPost("ResendOtp")]
        public async Task<IActionResult> ResendOtpAsync([FromBody] string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Phone number is required");
            }

            var result = await _authRepository.ResendOTPAsync(phoneNumber);
            if (!result.ActionSucceeded)
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
            var result = await _authRepository.ForgotPasswordAsync(phoneNumber);

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

            var result = await _authRepository.ResetPasswordAsync(model);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authRepository.GetTokenAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result);
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
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (userId == null)
            //{
            //    return NotFound("This Token Not Valid : ");
            //}


            //var phoneNumber = await _applicationUserRepository.GetUserPhoneNumber(userId);

            var result = await _authRepository.VerifyOTPAsync(model.OTP);

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
            var result = await _authRepository.DeleteUserAsync(userId);
            if (result.isDeleted == false)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);


        }
        [HttpPost("LogOut")]

        public async Task<IActionResult> LogOut()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }
            var result = await _authRepository.LogoutAsync(userId);
            if (result == "User Logged Out Successfully")
                return Ok(result);
            return BadRequest(result);
        }

        //[HttpPut("UpdateAccount")]

    }
}
