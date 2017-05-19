
using System.Linq;
using System.Collections.Generic;
using Notifications.Entities.Models;
using Repository.Pattern.Repositories;

namespace Notifications.Repository.Repositories
{
    public static class OrderRepository
    {
        public static IEnumerable<Order> GetOrdersByCustomerID(this IRepositoryAsync<Order> repository, int customerID)
        {
            return repository
                    .Queryable()
                    .Where(x => x.CustomerID == customerID)
                    .AsEnumerable();
        }

        public static Order GetMostRecentOrderByCustomerID(this IRepositoryAsync<Order> repository, int customerID)
        {
            return repository
                    .Queryable()
                    .Where(x => x.CustomerID == customerID)
                    .OrderByDescending(x => x.OrderDate)
                    .SingleOrDefault();
        }
    }
}
