using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBlockingService _blockService;
        private readonly IAuthService _authService;

        public UserController(IBlockingService blockService, IAuthService authService)
        {
            _blockService = blockService;
            _authService = authService;
        }

        //  [ServiceFilter(typeof(BlockingFilter))]


        // add this attribute on top of actions you want executed only for unblocked users

        [HttpPost("BlockUser")]
        public async Task<IActionResult> BlockUser(string blockedUserId)
        {
            var blockingUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (blockingUserId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }
            if (string.IsNullOrEmpty(blockedUserId))
            {
                return BadRequest("Missing required user ID.");
            }


            try
            {
                await _blockService.BlockUserAsync(blockingUserId, blockedUserId);
                return Ok("User successfully blocked.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("UnBlockUser")]
        public async Task<IActionResult> UnblockUser(string blockedUserId)
        {
            if (string.IsNullOrEmpty(blockedUserId))
            {
                return BadRequest("Missing required user ID.");
            }

            var blockingUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await _blockService.UnblockUserAsync(blockingUserId, blockedUserId);
                return Ok("User successfully unblocked.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("request-updating-phone-number")]
        public async Task<IActionResult> RequestUpdatingPhoneNumber([FromBody] UpdatePhoneNumberDTO updatePhoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }

            var result = await _authService.RequestUpdatingPhoneNumberAsync(userId, updatePhoneNumber);

            if (!result.IsUpdated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPut("confirm-phone-number-update")]
        public async Task<IActionResult> ConfirmPhoneNumberUpdate(string otp)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return NotFound("This Token Is Not Found : ");
            }

            var result = await _authService.ConfirmPhoneNumberUpdateAsync(userId, otp);

            if (!result.IsUpdated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
