
using Service.Pattern;
using System.Collections.Generic;
using Notifications.Entities.Models;
using Repository.Pattern.Repositories;
using Notifications.Repository.Repositories;

namespace Notifications.Services
{
    public interface IOrderService : IService<Order>
    {
        IEnumerable<Order> GetOrdersByCustomerID(int customerID);
    }

    public class OrderService : Service<Order>, IOrderService
    {
        private readonly IRepositoryAsync<Order> _repository;

        public OrderService(IRepositoryAsync<Order> repository) : base(repository)
        {
            _repository = repository;
        }
        public IEnumerable<Order> GetOrdersByCustomerID(int customerID)
        {
            return _repository.GetOrdersByCustomerID(customerID);
        }
        public override void Insert(Order entity)
        {
            // e.g. add business logic here before inserting
            base.Insert(entity);
        }
        public override void Delete(object id)
        {
            // e.g. add business logic here before deleting
            base.Delete(id);
        }
    }
}