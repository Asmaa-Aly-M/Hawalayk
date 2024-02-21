using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterCustomerAsync(RegisterCustomerModel model);
        Task<AuthModel> RegisterCraftsmanAsync(RegisterCraftsmanModel model);
    }
}
