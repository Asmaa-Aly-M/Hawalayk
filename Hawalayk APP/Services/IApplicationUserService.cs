using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IApplicationUserService
    {
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<string> GetUserPhoneNumber(string userId);
        Task<ApplicationUser> getCurrentUser(string userId);
    }
}