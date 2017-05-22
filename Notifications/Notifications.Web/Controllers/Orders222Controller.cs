
using System.Web.Mvc;
using Notifications.Services;
using Repository.Pattern.UnitOfWork;
using System.Threading.Tasks;
using Notifications.Entities.Models;
using System;
using Repository.Pattern.Infrastructure;

namespace Notifications.Controllers
{
    public class Orders22Controller : Controller
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ICustomerService _customerService;
        private readonly ISubscriberService _subscriptionService;
        private readonly IMenuService _menuService;
        private readonly ICompanyService _companyService;
        private readonly IOrderService _orderService;

        private const string SYS_USER = "SYSTEM";

        public Orders22Controller(IUnitOfWorkAsync unitOfWorkAsync,
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

        private const string _companyTextIdentifier = "LUNCH";
        // GET: Orders
        public ActionResult Index()
        {
            int companyId = _companyService.GetCompanyByTextIdentifier(_companyTextIdentifier).CompanyID;
            var orders = _orderService.GetOrdersByCompanyID(companyId);
            return View(orders);
        }

        public async Task Complete(int id)
        {
            try
            {
                var thisOrder = await _orderService.FindAsync(id);
                thisOrder.Status = OrderStatus.Completed;
                thisOrder.ModifiedBy = SYS_USER;
                thisOrder.ModifiedDate = DateTime.Now;
                thisOrder.ObjectState = ObjectState.Modified;
                _orderService.Update(thisOrder);
                _unitOfWorkAsync.SaveChanges();
            }
            catch { }
        }

        public async Task Cancel(int id)
        {
            try
            {
                var thisOrder = await _orderService.FindAsync(id);
                thisOrder.Status = OrderStatus.Cancelled;
                thisOrder.ModifiedBy = SYS_USER;
                thisOrder.ModifiedDate = DateTime.Now;
                thisOrder.ObjectState = ObjectState.Modified;
                _orderService.Update(thisOrder);
                _unitOfWorkAsync.SaveChanges();
            }
            catch { }
        }

        public async Task OnHold(int id)
        {
            try
            {
                var thisOrder = await _orderService.FindAsync(id);
                thisOrder.Status = OrderStatus.OnHold;
                thisOrder.ModifiedBy = SYS_USER;
                thisOrder.ModifiedDate = DateTime.Now;
                thisOrder.ObjectState = ObjectState.Modified;
                _orderService.Update(thisOrder);
                _unitOfWorkAsync.SaveChanges();
            }
            catch { }
        }
    }
}