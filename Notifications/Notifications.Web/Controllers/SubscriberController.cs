
using System;
using System.Linq;
using Twilio.TwiML;
using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Notifications.Helpers;
using System.Threading.Tasks;
using Notifications.Services;
using Repository.Pattern.UnitOfWork;
using Notifications.Entities.Models;
using System.Text.RegularExpressions;
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
        private readonly ICompanyService _companyService;
        private readonly IOrderService _orderService;

        private const string SYS_USER = "SYSTEM";

        public SubscriberController(IUnitOfWorkAsync unitOfWorkAsync,
                                    ISubscriberService subscriberService, 
                                    IMenuService menuService, 
                                    ICustomerService customerService, 
                                    ICompanyService companyService, 
                                    IOrderService orderService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _customerService = customerService;
            _subscriptionService = subscriberService;
            _menuService = menuService;
            _companyService = companyService;
            _orderService = orderService;
        }
        public async Task<TwiMLResult> Index()
        {
            var counter = 0;
            string requestBody = Request.Form["Body"].Trim();
            string requestFrom = Request.Form["From"];
            string requestTo = Request.Form["To"];
            var outputMessage = string.Empty;
            var messageCreator = new MessageCreator(_unitOfWorkAsync, _subscriptionService, _menuService);
            var orderCreator = new OrderCreator(_unitOfWorkAsync, _companyService, _menuService, _orderService);
            
            // get the session varible if it exists
            if (Session["counter"] != null)
            {
                counter = (int)Session["counter"];
            }
            counter++;
            Session["counter"] = counter;

            //get company data
            Company thisCompany = null;
            if (counter == 1) //first request, get the company details and step info
            {
                //var CreateCompanyDetailsFromRequest(requestBody)
                thisCompany = _companyService.GetCompanyByTextIdentifier(requestBody);
                if (thisCompany == null)
                {
                    counter--;
                    Session["counter"] = counter;
                    outputMessage = "You dialed a wrong number, Have a nice day :)";
                }
                else
                    Session["CompanyID"] = thisCompany.CompanyID;

            }
            if(Session["CompanyID"] != null)
            {
                int companyID = int.Parse(Session["CompanyID"].ToString());
                thisCompany = _companyService.GetCompanyByID(companyID);
            }
            if (thisCompany != null)
            {
                var thisSubscriber = await subscribeUser(thisCompany.CompanyID, requestFrom);
                switch(thisCompany.WorkFlowSteps.Count)
                {
                    case 2:  //2-step companies
                        if (counter == 1)
                        {
                            WorkflowStep step = thisCompany.WorkFlowSteps.First();
                            Regex expression = new Regex(step.RegularExpression);
                            if (expression.IsMatch(requestBody))
                            {
                                outputMessage = await messageCreator.Create(thisSubscriber, requestBody);
                            }
                            else
                            {
                                counter--;
                                Session["counter"] = counter;
                                outputMessage = "Bad input, kindly send another request :)";
                            }
                        }
                        else if (counter == 2)
                        {
                            WorkflowStep step = thisCompany.WorkFlowSteps.Last();
                            Regex expression = new Regex(step.RegularExpression);
                            if (expression.IsMatch(requestBody))
                            {
                                outputMessage = await orderCreator.CreateOrder(thisSubscriber.CustomerId, thisCompany.CompanyID, requestBody);
                                Session["counter"] = 0; // reseting the counter for fresh order for the same user
                            }
                            else
                            {
                                counter--;
                                Session["counter"] = counter;
                                outputMessage = "Bad input, kindly send another request :)";
                            }

                        }
                        else if (counter > 2)
                        {
                            outputMessage = string.Format("Your order is complete, Thanks for choosing {0}", thisCompany.CompanyName);
                        }

                            break;
                    case 3:  //3-step companies

                        break;
                    case 4:  //4-step companies

                        break;
                    default:

                        break;
                }
                
            }
            //call the subscription service
            //call the OrderCreator Helper
            //call the MessageCreator Helper

            var response = new MessagingResponse();
            response.Message(outputMessage);
            return TwiML(response);
        }

        private async Task<Subscriber> subscribeUser(int companyID, string requestFrom)
        {
            Subscriber subscriber = _subscriptionService.FindByPhoneNumber(requestFrom);
            if (subscriber == null)
            {
                //int companyId = _menuService.
                var customer = new Customer {
                    CompanyID = companyID,
                    Phone = requestFrom,
                    CreatedBy = SYS_USER,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = SYS_USER,
                    ModifiedDate = DateTime.Now,
                    ObjectState = ObjectState.Added
                };
                subscriber = new Subscriber {
                    CustomerId = customer.CustomerID,
                    PhoneNumber = requestFrom,
                    Subscribed = false,
                    IsActive = true,
                    CreatedBy = SYS_USER,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = SYS_USER,
                    ModifiedDate = DateTime.Now,
                    ObjectState = ObjectState.Added
                };
                _customerService.Insert(customer);
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
            return subscriber;
        }

        // POST: Subscribers/Register
        [HttpPost]
        public async Task<TwiMLResult> Register(string from, string body)
        {
            var phoneNumber = from;
            var message = body;

            var messageCreator = new MessageCreator(_unitOfWorkAsync, _subscriptionService, _menuService);
            var outputMessage = await messageCreator.Create(new Subscriber(), message);

            var response = new MessagingResponse();
            response.Message(outputMessage);

            return TwiML(response);
        }
    }
}