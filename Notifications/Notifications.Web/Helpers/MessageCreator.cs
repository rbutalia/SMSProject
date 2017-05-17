
using System;
using Notifications.Services;
using System.Threading.Tasks;
using Notifications.Entities.Models;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using System.Data.Entity.Infrastructure;

namespace Notifications.Helpers
{
    public class MessageCreator
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ICustomerService _customerService;
        private readonly ISubscriberService _subscriptionService;
        private readonly IMenuService _menuService;

        private const string SUBSCRIBE = "subscribe";
        private const string UNSUBSCRIBE = "unsubscribe";
        private const string SYS_USER = "SYSTEM";

        public MessageCreator(IUnitOfWorkAsync unitOfWorkAsync, ICustomerService customerService, ISubscriberService subscriptionService, IMenuService menuService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _subscriptionService = subscriptionService;
            _customerService = customerService;
            _menuService = menuService;
        }

        public async Task<string> Create(string phoneNumber, string message)
        {
            var subscriber = new Subscriber();
            return await CreateOutputMessage(subscriber, message);

        }

        private async Task<string> CreateOutputMessage(Subscriber subscriber, string message)
        {
            string returnMessage;
            if (!IsValidCommand(message))
            {
                return "Sorry, we don't recognize that command. Available commands are: 'add' or 'remove'.";
            }

            switch(message.ToLower())
            {
                case SUBSCRIBE:
                    subscriber.Subscribed = true;
                    subscriber.ModifiedBy = SYS_USER;
                    subscriber.ModifiedDate = DateTime.Now;
                    subscriber.ObjectState = ObjectState.Modified;
                    _subscriptionService.Update(subscriber);
                    try
                    {
                        await _unitOfWorkAsync.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    returnMessage = "You are now subscribed for updates.";
                    break;
                case UNSUBSCRIBE:
                    subscriber.Subscribed = false;
                    subscriber.ModifiedBy = SYS_USER;
                    subscriber.ModifiedDate = DateTime.Now;
                    subscriber.ObjectState = ObjectState.Modified;
                    _subscriptionService.Update(subscriber);
                    try
                    {
                        await _unitOfWorkAsync.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    returnMessage = "You have unsubscribed from notifications. Test 'add' to start receiving updates again";
                    break;
                default:
                    returnMessage = _menuService.GetMenuByCompanyIdentifier(message);
                    break;
            }
            return returnMessage;
        }

        private static bool IsValidCommand(string command)
        {
            return command.StartsWith(SUBSCRIBE) || command.StartsWith(UNSUBSCRIBE);
        }
    }
}