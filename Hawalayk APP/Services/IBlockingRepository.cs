namespace Hawalayk_APP.Services
{
    public interface IBlockingRepository
    {
        Task BlockUserAsync(string blockingUserId, string blockedUserId);
        Task<bool> IsUserBlockedAsync(string blockingUserId, string blockedUserId);
        Task UnblockUserAsync(string blockingUserId, string blockedUserId);
    }
}
