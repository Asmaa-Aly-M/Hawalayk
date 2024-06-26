﻿using Hawalayk_APP.Helpers;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Hawalayk_APP.Services
{
    public class SMSRepository : ISMSRepository
    {

        private readonly TwilioSettings _twilio;
        public SMSRepository(IOptions<TwilioSettings> twilio)
        {
            _twilio = twilio.Value;
        }
        public async Task<MessageResource> SendSMS(string PhoneNumber, string Body)
        {
            TwilioClient.Init(_twilio.AccountSID, _twilio.AuthToken);
            var result = await MessageResource.CreateAsync(
                body: Body,
                from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                to: PhoneNumber
                );
            return result;
        }

        public string GenerateOTP(bool IsAlphanumeric, int length)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            if (IsAlphanumeric)
            {
                characters += alphabets + small_alphabets + numbers;
            }

            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }

            return otp;
        }


        public async Task SendCraftsmanApprovalNotification(string phoneNumber, bool isApproved)
        {
            string messageBody;
            if (isApproved)
            {
                messageBody = $"Congratulations! Your registration has been approved. You can now access the platform.";
            }
            else
            {
                messageBody = $"We regret to inform you that your registration has been rejected. Please contact us for more information.";
            }

            await SendSMS(phoneNumber, messageBody);
        }


    }
}
