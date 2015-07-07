using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Twilio;

namespace SuperHeroi.Infra.Identity.IdentityStart
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            if (ConfigurationManager.AppSettings["Internet"] == "true")
            {
                // Utilizando TWILIO como SMS Provider.
                // https://www.twilio.com/docs/quickstart/csharp/sms/sending-via-rest

                const string accountSid = "AC8b2a3b8c52fed3698c9f61486e34cef6";
                const string authToken = "c1a8f8a3536628e1685271edf9ebca80";

                var client = new TwilioRestClient(accountSid, authToken);

                client.SendMessage("646-759-9194", message.Destination, message.Body);
            }

            return Task.FromResult(0);
        }
    }
}