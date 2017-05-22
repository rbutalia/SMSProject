
using System;
using System.Text;
using Notifications.Services;
using System.Threading.Tasks;
using Notifications.Entities.Models;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;

namespace Notifications.Helpers
{
    public class OrderCreator
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ICompanyService _companyService;
        private readonly IMenuService _menuService;
        private readonly IOrderService _orderService;

        private const string SYS_USER = "SYSTEM";

        public OrderCreator(IUnitOfWorkAsync unitOfWorkAsync,
                            ICompanyService companyService,
                            IMenuService menuService,
                            IOrderService orderService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _companyService = companyService;
            _menuService = menuService;
            _orderService = orderService;
        }

        public async Task<string> CreateOrder(int customerID, int companyID, string message){
            try {
                _unitOfWorkAsync.BeginTransaction();
                var newOrder = new Order { CustomerID = customerID,
                                           CompanyID = companyID,
                                           OrderDate = DateTime.Now,
                                           Status = OrderStatus.Pending,
                                           CreatedBy = SYS_USER,
                                           CreatedDate = DateTime.Now,
                                           ModifiedBy = SYS_USER,
                                           ModifiedDate = DateTime.Now,
                                           ObjectState = ObjectState.Added };

                _orderService.Insert(newOrder);
                var items = message.Split(',');
                var flag = VerifyOrderInput(items);
                if (flag)
                {
                    decimal total = 0.0m;
                    decimal taxRate = 1 + _companyService.GetCompanyTaxRateByCompanyID(companyID) / 100;
                    foreach (var item in items)
                    {
                        var menu = _menuService.GetMenuByCompanyID(companyID);
                        var price = _menuService.GetMenuPriceByMenuItemID(companyID, int.Parse(item));
                        if (price == 0.0m)
                            return await CreateOutputMessage(ReturnStatus.ItemNotFound, int.Parse(item));
                        var itemDetail = new OrderDetail
                        {
                            OrderID = newOrder.OrderID,
                            MenuItemID = int.Parse(item),
                            UnitPrice = price,
                            Quantity = 1,
                            CreatedBy = SYS_USER,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = SYS_USER,
                            ModifiedDate = DateTime.Now,
                            ObjectState = ObjectState.Added
                        };
                        total += price;
                        newOrder.OrderDetails.Add(itemDetail);
                    }
                    newOrder.Total = total;
                    newOrder.TotalWithTax = total * taxRate; ;
                    _orderService.InsertOrUpdateGraph(newOrder);
                    _unitOfWorkAsync.SaveChanges();
                    _unitOfWorkAsync.Commit();
                    return await CreateOutputMessage(ReturnStatus.Success, newOrder.OrderID);
                }
                else {
                    return await CreateOutputMessage(ReturnStatus.InvalidInput, null);
                }
            }
            catch (Exception ex) {
                return await CreateOutputMessage(ReturnStatus.Failure, null);
            }
        }

        private bool VerifyOrderInput(string[] items){
            try{
                foreach (var item in items){
                    int itemResult;
                    if (!int.TryParse(item, out itemResult))
                        return false;
                }
                return true;
            }
            catch{
                return false;
            }
        }

        private async Task<string> CreateOutputMessage(ReturnStatus status, int? orderId){
            var outputMesssage = string.Empty;
            switch(status)
            {
                case ReturnStatus.Success:
                    var thisOrder = await _orderService.FindAsync(orderId);
                    var result = new StringBuilder();
                    result.AppendLine(string.Format("Order # {0} successfully placed.", orderId));
                    result.AppendLine(string.Format("Your order total is {0}$", Math.Round(thisOrder.TotalWithTax, 2)));
                    outputMesssage = result.ToString();
                    break;
                case ReturnStatus.Failure:
                    outputMesssage = "Sorry for the inconvenience, system unable to place the order.";
                    break;
                case ReturnStatus.InvalidInput:
                    outputMesssage = "Invalid input, system unable to process the request.";
                    break;
                default:
                case ReturnStatus.ItemNotFound:
                    outputMesssage = "Menu item not found, system unable to place the order.";
                    break;
            }
            return outputMesssage;
        }
    }
}