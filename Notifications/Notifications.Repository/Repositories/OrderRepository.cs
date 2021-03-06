﻿
using System.Linq;
using System.Collections.Generic;
using Notifications.Entities.Models;
using Repository.Pattern.Repositories;

namespace Notifications.Repository.Repositories
{
    public static class OrderRepository
    {
        public static Order GetOrderByOrderID(this IRepositoryAsync<Order> repository, int orderID)
        {
            return repository
                     .Query(x => x.OrderID == orderID)
                     .Include(x => x.Customer)
                     .Select().SingleOrDefault();
        }
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
        public static IEnumerable<Order> GetOrdersByCompanyID(this IRepositoryAsync<Order> repository, int companyID)
        {
            return repository
                     .Query(x => x.CompanyID == companyID && x.Status == OrderStatus.Pending)
                     .Include(x => x.Customer)
                     .Include(x => x.OrderDetails)
                     .Select();
        }
    }
}
