using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IBanRepository
    {
        Task CreateAsync(string userId, int banDurationInMinutes);
        Task UnbanUserAsync(string userId);
        Task<bool> IsUserBannedAsync(string userId);
        Task<List<ApplicationUser>> GetBannedUsersAsync();
    }
}
