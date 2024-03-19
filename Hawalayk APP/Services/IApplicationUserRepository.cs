namespace Hawalayk_APP.Services
{
    public interface IApplicationUserRepository
    {
        Task<string> GetUserPhoneNumber(string userId);
    }
}
