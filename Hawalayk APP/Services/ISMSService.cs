using Twilio.Rest.Api.V2010.Account;

namespace Hawalayk_APP.Services
{
    public interface ISMSService
    {
        MessageResource SendSMS(string PhoneNumber, string Body);
    }
}
