using Twilio.Rest.Api.V2010.Account;

namespace Hawalayk_APP.Services
{
    public interface ISMSService
    {
        Task<MessageResource> SendSMS(string PhoneNumber, string Body);
        public string GenerateOTP(bool IsAlphanumeric, int length);
        Task SendCraftsmanApprovalNotification(string phoneNumber, bool isApproved);
    }
}
