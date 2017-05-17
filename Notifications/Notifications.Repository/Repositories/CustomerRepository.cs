
using System.Linq;
using System.Collections.Generic;
using Notifications.Entities.Models;
using Notifications.Repository.Models;
using Repository.Pattern.Repositories;

namespace Notifications.Repository.Repositories
{
    public static class CustomerRepository
    {
        public static decimal GetCustomerOrderTotalByYear(this IRepository<Customer> repository, int customerID, int year)
        {
            return repository
                    .Queryable()
                    .Where(c => c.CustomerID == customerID)
                    .SelectMany(c => c.Orders.Where(o => o.OrderDate != null && o.OrderDate.Year == year))
                    .SelectMany(c => c.OrderDetails)
                    .Select(c => c.Quantity * c.UnitPrice)
                    .Sum();
        }

        public static IEnumerable<Customer> CustomersByCompanyId(this IRepositoryAsync<Customer> repository, int companyId)
        {
            return repository
                    .Queryable()
                    .Where(x => x.Company.CompanyID == companyId)
                    .AsEnumerable();
        }

        public static IEnumerable<CustomerOrder> GetCustomerOrder(this IRepository<Customer> repository, string country)
        {
            var customers = repository.GetRepository<Customer>().Queryable();
            var orders = repository.GetRepository<Order>().Queryable();

            var query = from c in customers
                        join o in orders on new { a = c.CustomerID, b = c.Country }
                            equals new { a = o.CustomerID, b = country }
                        select new CustomerOrder
                        {
                            CustomerId = c.CustomerID,
                            ContactName = c.ContactName,
                            OrderId = o.OrderID,
                            OrderDate = o.OrderDate
                        };

            return query.AsEnumerable();
        }
    }
}
