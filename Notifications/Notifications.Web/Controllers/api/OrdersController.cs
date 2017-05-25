
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Notifications.Services;
using System.Collections.Generic;
using Repository.Pattern.UnitOfWork;
using Notifications.Entities.Models;
using Repository.Pattern.Infrastructure;

namespace Notifications.Controllers.api
{
    public class OrdersController : ApiController
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ICustomerService _customerService;
        private readonly ISubscriberService _subscriptionService;
        private readonly IMenuService _menuService;
        private readonly ICompanyService _companyService;
        private readonly IOrderService _orderService;

        private const string SYS_USER = "SYSTEM_API";
        private const string _companyTextIdentifier = "LUNCH";

        public OrdersController(IUnitOfWorkAsync unitOfWorkAsync,
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

        // GET api/<controller>
        public IEnumerable<Order> Get()
        {
            var thisCompany = _companyService.GetCompanyByTextIdentifier(_companyTextIdentifier);
            if (thisCompany == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Company not found for identifier {0}", _companyTextIdentifier)),
                    ReasonPhrase = "Company not found"
                };
                throw new HttpResponseException(response);
            }
            var companyId = thisCompany.CompanyID;
            var orders = _orderService.GetOrdersByCompanyID(companyId);
            return orders;
        }

        // GET api/<controller>/5
        public async Task<Order> Get(int id)
        {
            return await _orderService.FindAsync(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            try
            {
                var thisOrder = _orderService.Find(id);
                if (thisOrder == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("OrderID {0} not found.", id)),
                        ReasonPhrase = "Order not found"
                    };
                    throw new HttpResponseException(response);
                }
                if (value == "Complete")
                    thisOrder.Status = OrderStatus.Completed;
                else if(value == "Cancel")
                    thisOrder.Status = OrderStatus.Cancelled;
                thisOrder.ModifiedBy = SYS_USER;
                thisOrder.ModifiedDate = DateTime.Now;
                thisOrder.ObjectState = ObjectState.Modified;
                _orderService.Update(thisOrder);
                _unitOfWorkAsync.SaveChanges();
            }
            catch { }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}