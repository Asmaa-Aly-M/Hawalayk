using Hawalayk_APP.Helpers;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Api.V2010.Account.AvailablePhoneNumberCountry;

namespace Hawalayk_APP.Services
{
    public class SMSService : ISMSService
    {

        private readonly TwilioSettings _twilio;
        public SMSService(IOptions<TwilioSettings> twilio)
        {
            _twilio = twilio.Value;
        }
        public MessageResource SendSMS(string PhoneNumber, string Body)
        {
            TwilioClient.Init(_twilio.AccountSID, _twilio.AuthToken);
            var result = MessageResource.Create(
                body :Body , 
                from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                to: PhoneNumber 
                );
            return result;
        }
    }
}
