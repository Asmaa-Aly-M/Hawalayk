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

        public UserController(IBlockingService blockService)
        {
            _blockService = blockService;
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

    }
}
