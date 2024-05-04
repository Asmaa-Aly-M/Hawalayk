using Twilio.Rest.Api.V2010.Account;

namespace Hawalayk_APP.Services
{
    public interface ISMSService
    {
        MessageResource SendSMS(string PhoneNumber, string Body);
        public string GenerateOTP(bool IsAlphanumeric, int length);
        void SendCraftsmanApprovalNotification(string phoneNumber, bool isApproved);
    }
}
