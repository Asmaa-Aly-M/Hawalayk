using Hawalayk_APP.Context;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext Context;

        //public AuthService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext, IOptions<JWT> jwt, ISMSService smsService)

        public ApplicationUserRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext _Context)
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
        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            ApplicationUser ApplicationUser = await Context.ApplicationUsers.FirstOrDefaultAsync(s => s.Id == id);
            return ApplicationUser;
        }
        public async Task<ApplicationUser> getCurrentUser(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}
