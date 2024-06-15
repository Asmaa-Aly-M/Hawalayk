using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class BlockingFilter : IAsyncActionFilter
{
    private readonly IBlockingRepository _blockingRepository;

    public BlockingFilter(IBlockingRepository blockingRepository)
    {
        _blockingRepository = blockingRepository;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var blockingUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (blockingUserId == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (context.ActionArguments.TryGetValue("blockedUserId", out var blockedUserIdObj) &&
            blockedUserIdObj is string blockedUserId)
        {
            var isBlocked = await _blockingRepository.IsUserBlockedAsync(blockingUserId, blockedUserId);
            if (isBlocked)
            {
                context.Result = new ForbidResult();
                return;
            }
        }

        await next();
    }
}