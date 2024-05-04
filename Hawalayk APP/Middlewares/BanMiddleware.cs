using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;

namespace Hawalayk_APP.Middlewares
{
    public class BanMiddleware : IMiddleware
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public BanMiddleware(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var user = await _userManager.GetUserAsync(context.User);
            if (user != null && user.IsBanned)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("You have been banned from using the app.");
                return;
            }
            await next(context);
        }
    }
}
