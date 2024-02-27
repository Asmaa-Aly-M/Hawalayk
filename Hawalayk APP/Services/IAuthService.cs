using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterCustomerAsync(RegisterCustomerModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthModel> RegisterCraftsmanAsync(RegisterCraftsmanModel model);
        Task<AuthModel> VerifyOTPAsync(string phoneNumber, string otp);

    }
}
