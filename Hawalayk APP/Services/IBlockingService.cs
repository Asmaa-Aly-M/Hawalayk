namespace Hawalayk_APP.Services
{
    public interface IBlockingService
    {
        Task BlockUserAsync(string blockingUserId, string blockedUserId);
        Task<bool> IsUserBlockedAsync(string blockingUserId, string blockedUserId);
        Task UnblockUserAsync(string blockingUserId, string blockedUserId);
    }
}
