using Hawalayk_APP.DataTransferObject;

namespace Hawalayk_APP.Services
{
    public interface IBlockingRepository
    {
        Task BlockUserAsync(string blockingUserId, string blockedUserId);
        Task<bool> IsUserBlockedAsync(string blockingUserId, string blockedUserId);
        Task UnblockUserAsync(string blockingUserId, string blockedUserId);
        Task<List<string>> GetBlockedUsersAsync(string userId);
        Task<List<BlockedUserDTO>> GetMyBlockedUsersAsync(string userId);
    }
}
