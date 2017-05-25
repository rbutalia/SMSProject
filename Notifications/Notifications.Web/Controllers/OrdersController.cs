
using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using Notifications.Services;
using Repository.Pattern.UnitOfWork;
using Notifications.Entities.Models;
using Repository.Pattern.Infrastructure;

namespace Notifications.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ICustomerService _customerService;
        private readonly ISubscriberService _subscriptionService;
        private readonly IMenuService _menuService;
        private readonly ICompanyService _companyService;
        private readonly IOrderService _orderService;
        private readonly INotificationService _smsService;

        private const string SYS_USER = "SYSTEM";

        public OrdersController(IUnitOfWorkAsync unitOfWorkAsync,
                                    ISubscriberService subscriberService,
                                    IMenuService menuService,
                                    ICustomerService customerService,
                                    ICompanyService companyService,
                                    IOrderService orderService,
                                    INotificationService smsService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _customerService = customerService;
            _subscriptionService = subscriberService;
            _menuService = menuService;
            _companyService = companyService;
            _orderService = orderService;
            _smsService = smsService;
        }

        private const string _companyTextIdentifier = "LUNCH";
        // GET: Orders
        public ActionResult Index()
        {
            int companyId = _companyService.GetCompanyByTextIdentifier(_companyTextIdentifier).CompanyID;
            var orders = _orderService.GetOrdersByCompanyID(companyId);
            return View(orders);
        }

        public ActionResult Complete(int id)
        {
            try
            {
                var thisOrder = _orderService.GetOrderByOrderID(id);
                var confimationMessage = string.Format("Your order {0} is ready for pickup.", thisOrder.OrderID);
                thisOrder.Status = OrderStatus.Completed;
                thisOrder.ModifiedBy = SYS_USER;
                thisOrder.ModifiedDate = DateTime.Now;
                thisOrder.ObjectState = ObjectState.Modified;
                _orderService.Update(thisOrder);
                _unitOfWorkAsync.SaveChanges();
                _smsService.SendMessage(thisOrder.Customer.Phone, confimationMessage, null);
                
            }
            catch(Exception ex)
            {
                var som = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Cancel(int id)
        {
            try
            {
                var thisOrder = _orderService.Find(id);
                thisOrder.Status = OrderStatus.Cancelled;
                thisOrder.ModifiedBy = SYS_USER;
                thisOrder.ModifiedDate = DateTime.Now;
                thisOrder.ObjectState = ObjectState.Modified;
                _orderService.Update(thisOrder);
                _unitOfWorkAsync.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { }
            return null;
        }

        public ActionResult OnHold(int id)
        {
            try
            {
                var thisOrder = _orderService.Find(id);
                thisOrder.Status = OrderStatus.OnHold;
                thisOrder.ModifiedBy = SYS_USER;
                thisOrder.ModifiedDate = DateTime.Now;
                thisOrder.ObjectState = ObjectState.Modified;
                _orderService.Update(thisOrder);
                _unitOfWorkAsync.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { }
            return null;
        }
    }
}