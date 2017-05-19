
using System.Web.Mvc;
using Notifications.Services;
using Repository.Pattern.UnitOfWork;

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

        private const string SYS_USER = "SYSTEM";

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

        private const string _companyTextIdentifier = "LUNCH";
        // GET: Orders
        public ActionResult Index()
        {
            int companyId = _companyService.GetCompanyByTextIdentifier(_companyTextIdentifier).CompanyID;
            var orders = _orderService.GetOrdersByCompanyID(companyId);
            return View(orders);
        }
    }
}