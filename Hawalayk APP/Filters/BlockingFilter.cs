//using Hawalayk_APP.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System.Security.Claims;

//public class BlockingFilter : IAsyncActionFilter
//{
 //   private readonly IBlockingRepository _blockingRepository;

 //   public BlockingFilter(IBlockingRepository blockingRepository)
 //  {
 //      _blockingRepository = blockingRepository;
 //   }

//    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//    {
//        var blockingUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        if (blockingUserId == null)
//        {
//            context.Result = new UnauthorizedResult();
//            return;
//        }

//        if (context.ActionArguments.TryGetValue("blockedUserId", out var blockedUserIdObj) &&
//            blockedUserIdObj is string blockedUserId)
//        {
//            var isBlocked = await _blockingRepository.IsUserBlockedAsync(blockingUserId, blockedUserId);
//            if (isBlocked)
//            {
//                context.Result = new ForbidResult();
//                return;
//            }
//        }

//        await next();
//    }
//}


using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//using Hawalayk_APP.Attributes;

public class BlockingFilter : IAsyncActionFilter
{
    private readonly IBlockingRepository _blockingRepository;

   public BlockingFilter(IBlockingRepository blockingRepository)
   {
       _blockingRepository = blockingRepository;
   }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
   {
        /* // Retrieve the parameter name for blockedUserId from the action metadata
         var parameterName = GetBlockedUserIdParameterName(context);

         // Check if the action has the parameter for blockedUserId
         if (parameterName != null && context.ActionArguments.TryGetValue(parameterName, out var userIdObj) && userIdObj is string userId)
         {
             // Perform blocking check
             var blockingUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

             if (blockingUserId != null)
             {
                 var isBlocked = await _blockingRepository.IsUserBlockedAsync(blockingUserId, userId);
                 if (isBlocked)
                 {
                     context.Result = new ForbidResult();
                     return;
                 }
             }
         }

         await next();*/
    }

    /* private string GetBlockedUserIdParameterName(ActionExecutingContext context)
    {
        // Retrieve the custom attribute applied to the action
        var blockCheckAttribute = context.ActionDescriptor.EndpointMetadata
            .OfType<BlockCheckAttribute>()
            .FirstOrDefault();

        return blockCheckAttribute?.ParameterName;
    }*/
}