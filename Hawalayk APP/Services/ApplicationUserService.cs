using Hawalayk_APP.Context;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;

namespace Hawalayk_APP.Services
{
    public class ApplicationUserService : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        //public AuthService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext, IOptions<JWT> jwt, ISMSService smsService)

        public ApplicationUserService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;

        }
        public async Task<string> GetUserPhoneNumber(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //  await _userManager.LoadAsync(user); // Ensure PhoneNumber property is loaded
                return user.PhoneNumber;
            }
            return null; // User not found
        }
    }
}
