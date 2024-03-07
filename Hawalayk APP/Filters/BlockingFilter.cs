using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Hawalayk_APP.Filters
{
    public class BlockingFilter:IAsyncAuthorizationFilter
    {
        private readonly IBlockingService _blockingService;

        public BlockingFilter(IBlockingService blockingService)
        {
            _blockingService = blockingService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var blockingUserId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var blockedUserId = context.RouteData.Values["id"]?.ToString();

            if (!string.IsNullOrEmpty(blockedUserId))
            {
                if (await _blockingService.IsUserBlockedAsync(blockingUserId, blockedUserId))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }
    }
}
