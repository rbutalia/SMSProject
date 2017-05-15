
using System;
using Notifications.Services;
using System.Threading.Tasks;
using Notifications.Entities.Models;

namespace Notifications.Helpers
{
    public class MessageCreator
    {
        private readonly ISubscriberService _subscriptionService;
        private const string SUBSCRIBE = "add";
        private const string UNSUBSCRIBE = "remove";

        public MessageCreator(ISubscriberService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public async Task<string> Create(string phoneNumber, string message)
        {
            var subscriber = _subscriptionService.FindByPhoneNumber(phoneNumber);
            if (subscriber != null)
            {
                return await CreateOutputMessage(subscriber, message.ToLower());
            }

            subscriber = new Subscriber
            {
                PhoneNumber = phoneNumber,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            _subscriptionService.Insert(subscriber);
            return "Thanks for contacting TWBC! Text 'add' if you would to receive updates via text message.";
        }

        private async Task<string> CreateOutputMessage(Subscriber subscriber, string message)
        {
            if (!IsValidCommand(message))
            {
                return "Sorry, we don't recognize that command. Available commands are: 'add' or 'remove'.";
            }

            var isSubscribed = message.StartsWith(SUBSCRIBE);
            subscriber.Subscribed = isSubscribed;
            subscriber.UpdatedOn = DateTime.Now;
            _subscriptionService.Update(subscriber);

            return isSubscribed
                ? "You are now subscribed for updates."
                : "You have unsubscribed from notifications. Test 'add' to start receiving updates again";
        }

        private static bool IsValidCommand(string command)
        {
            return command.StartsWith(SUBSCRIBE) || command.StartsWith(UNSUBSCRIBE);
        }
    }
}