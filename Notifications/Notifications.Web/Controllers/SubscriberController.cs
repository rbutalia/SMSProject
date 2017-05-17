
using System;
using Twilio.TwiML;
using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Notifications.Helpers;
using System.Threading.Tasks;
using Notifications.Services;
using Repository.Pattern.UnitOfWork;
using Notifications.Entities.Models;
using System.Data.Entity.Infrastructure;
using Repository.Pattern.Infrastructure;

namespace Notifications.Controllers
{
    public class SubscriberController : TwilioController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ICustomerService _customerService;
        private readonly ISubscriberService _subscriptionService;
        private readonly IMenuService _menuService;
        private const string SYS_USER = "SYSTEM";

        public SubscriberController(IUnitOfWorkAsync unitOfWorkAsync,
                                    ISubscriberService subscriberService, 
                                    IMenuService menuService, 
                                    ICustomerService customerService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _customerService = customerService;
            _subscriptionService = subscriberService;
            _menuService = menuService;
        }
        public async Task<TwiMLResult> Index()
        {
            var counter = 0;
            string requestBody = Request.Form["Body"];
            string requestFrom = Request.Form["From"];
            string requestTo = Request.Form["To"];
            
            // get the session varible if it exists
            if (Session["counter"] != null)
            {
                counter = (int)Session["counter"];
            }
            counter++;
            Session["counter"] = counter;

            // verify if the subscriber exists, if not add him to the subscriber list
            var subscriber = _subscriptionService.FindByPhoneNumber(requestFrom);
            if (subscriber == null)
            {
                //int companyId = _menuService.
                //var customer = new Customer { companyID };
                subscriber = new Subscriber
                {
                    PhoneNumber = requestFrom,
                    Subscribed = false,
                    IsActive = true,
                    CreatedBy = SYS_USER,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = SYS_USER,
                    ModifiedDate = DateTime.Now,
                    ObjectState = ObjectState.Added
                };
                _subscriptionService.Insert(subscriber);
                try
                {
                    await _unitOfWorkAsync.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!CustomerExists(key))
                    //{
                    //    return NotFound();
                    //}
                    throw;
                }
            }
            var messageCreator = new MessageCreator(_unitOfWorkAsync, _customerService, _subscriptionService, _menuService);
            var outputMessage = await messageCreator.Create(requestFrom, "LUNCH");
            var response = new MessagingResponse();
            response.Message(outputMessage);
            return TwiML(response);
        }

        // POST: Subscribers/Register
        [HttpPost]
        public async Task<TwiMLResult> Register(string from, string body)
        {
            var phoneNumber = from;
            var message = body;

            var messageCreator = new MessageCreator(_unitOfWorkAsync, _customerService, _subscriptionService, _menuService);
            var outputMessage = await messageCreator.Create(phoneNumber, message);

            var response = new MessagingResponse();
            response.Message(outputMessage);

            return TwiML(response);
        }
    }
}