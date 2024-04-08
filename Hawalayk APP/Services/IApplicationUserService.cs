using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IApplicationUserService
    {
        ApplicationUser GetById(string id);
        Task<string> GetUserPhoneNumber(string userId);
    }
}