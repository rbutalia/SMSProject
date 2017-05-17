
using Twilio.TwiML;
using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Notifications.Helpers;
using System.Threading.Tasks;
using Notifications.Services;
using Repository.Pattern.UnitOfWork;

namespace Notifications.Controllers
{
    public class SubscriberController : TwilioController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ISubscriberService _subscriberService;
        //private readonly IMenuService _menuService;


        public SubscriberController(ISubscriberService subscriberService, IUnitOfWorkAsync unitOfWorkAsync)//, IMenuService menuService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _subscriberService = subscriberService;
            //_menuService = menuService;
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
            var messageCreator = new MessageCreator(_subscriberService, _unitOfWorkAsync);//, _menuService);
            var outputMessage = await messageCreator.Create(requestFrom, requestBody);
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

            var messageCreator = new MessageCreator(_subscriberService, _unitOfWorkAsync);//, _menuService);
            var outputMessage = await messageCreator.Create(phoneNumber, message);

            var response = new MessagingResponse();
            response.Message(outputMessage);

            return TwiML(response);
        }
    }
}