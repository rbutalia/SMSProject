
using System;
using Twilio;
using Twilio.Types;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using Twilio.Rest.Api.V2010.Account;

namespace Notifications.Services
{
    public interface INotificationService
    {
        MessageResource SendMessage(string to, string body, List<Uri> mediaUrl);
        Task<MessageResource> SendMessageAsync(string to, string body, List<Uri> mediaUrl);
    }

    public class NotificationService : INotificationService
    {
        public NotificationService()
        {
            var sid = ConfigurationManager.AppSettings["Twilio_SID"];
            var authToken = ConfigurationManager.AppSettings["Twilio_AuthToken"];
            if (sid != null && authToken != null)
            {
                TwilioClient.Init(sid, authToken);
            }
        }
        public MessageResource SendMessage(string to, string body, List<Uri> mediaUrl)
        {
            return MessageResource.Create(
                from: new PhoneNumber(ConfigurationManager.AppSettings["Twilio_Phone_Number"]),
                to: new PhoneNumber(to),
                body: body,
                mediaUrl: mediaUrl);
        }
        public async Task<MessageResource> SendMessageAsync(string to, string body, List<Uri> mediaUrl)
        {
            return await MessageResource.CreateAsync(
                from: new PhoneNumber(ConfigurationManager.AppSettings["Twilio_Phone_Number"]),
                to: new PhoneNumber(to),
                body: body,
                mediaUrl: mediaUrl);
        }
    }
}