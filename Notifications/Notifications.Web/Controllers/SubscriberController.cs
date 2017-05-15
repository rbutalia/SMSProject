
using Twilio.TwiML;
using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Notifications.Helpers;
using System.Threading.Tasks;
using Notifications.Services;

namespace Notifications.Controllers
{
    public class SubscriberController : TwilioController
    {
        private readonly ISubscriberService _subscriberService;

        //public SubscriberController() : this(new SubscriberService()) { }

        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        public async Task<TwiMLResult> Index()
        {
            var messageCreator = new MessageCreator(_subscriberService);
            var outputMessage = await messageCreator.Create("+17165414925", "some message");

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

            var messageCreator = new MessageCreator(_subscriberService);
            var outputMessage = await messageCreator.Create(phoneNumber, message);

            var response = new MessagingResponse();
            response.Message(outputMessage);

            return TwiML(response);
        }
    }
}