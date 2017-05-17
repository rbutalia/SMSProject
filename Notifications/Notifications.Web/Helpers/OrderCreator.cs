﻿
using System;
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
        //private readonly ICustomerService _customerService;
        //private readonly ISubscriberService _subscriptionService;
        //private readonly IMenuService _menuService;
        private readonly IOrderService _orderService;

        private const string SYS_USER = "SYSTEM";

        public OrderCreator(IUnitOfWorkAsync unitOfWorkAsync, 
                            IOrderService orderService){
            _unitOfWorkAsync = unitOfWorkAsync;
            //_subscriptionService = subscriptionService;
            //_customerService = customerService;
            //_menuService = menuService;
            _orderService = orderService;
        }

        public async Task<string> CreateOrder(int customerID, string message){
            try {
                var newOrder = new Order { CustomerID = customerID, OrderDate = DateTime.Now, CreatedBy = SYS_USER, CreatedDate = DateTime.Now, ModifiedBy = SYS_USER, ModifiedDate = DateTime.Now, ObjectState = ObjectState.Added };
                var items = message.Split(',');
                foreach (var item in items)
                {
                    var itemDetail = new OrderDetail { OrderID = newOrder.OrderID, MenuItemID = int.Parse(item), Quantity = 1, CreatedBy = SYS_USER, CreatedDate = DateTime.Now, ModifiedBy = SYS_USER, ModifiedDate = DateTime.Now, ObjectState = ObjectState.Added };
                    newOrder.OrderDetails.Add(itemDetail);
                }
                _orderService.InsertOrUpdateGraph(newOrder);
                _unitOfWorkAsync.SaveChanges();
                return await CreateOutputMessage(true);
            }
            catch (Exception) {
            }
            return await CreateOutputMessage(false);
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

        private async Task<string> CreateOutputMessage(bool status){
            var outputMesssage = string.Empty;
            if (status)
                return "Order successfully placed";
            return "Sorry for the inconveniencee, The system was unable to place an order at the present time.";
        }
    }
}