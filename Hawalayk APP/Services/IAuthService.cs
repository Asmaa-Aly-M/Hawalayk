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

        Task<AuthModel> ForgotPasswordAsync(string phoneNumber);
        Task<AuthModel> ResetPasswordAsync(ResetPasswordModel model);

        Task<DeleteUserDTO> DeleteUserAsync(string userId);
        Task<string> LogoutAsync(string userId);

    }
}
