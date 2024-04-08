using Hawalayk_APP.Context;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;

namespace Hawalayk_APP.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext Context;

        //public AuthService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext, IOptions<JWT> jwt, ISMSService smsService)

        public ApplicationUserService(UserManager<ApplicationUser> userManager, ApplicationDbContext _Context)
        {
            _userManager = userManager;
            Context = _Context;

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
        public ApplicationUser GetById(string id)
        {
            ApplicationUser ApplicationUser = Context.ApplicationUsers.FirstOrDefault(s => s.Id == id);
            return ApplicationUser;
        }
    }
}
