
using Notifications.Entities.Models;
using Repository.Pattern.Ef6;

namespace Notifications.Tests.UnitTests.Fake
{
    public class NotificationsFakeContext : FakeDbContext
    {
        public NotificationsFakeContext()
        {
            AddFakeDbSet<Category, CategoryDbSet>();
            AddFakeDbSet<Customer, CustomerDbSet>();
            AddFakeDbSet<Order, OrderDbSet>();
            AddFakeDbSet<OrderDetail, OrderDetailDbSet>();
            AddFakeDbSet<Supplier, SupplierDbSet>();
            AddFakeDbSet<Product, ProductDbSet>();
            AddFakeDbSet<Shipper, ShippperDbSet>();
            AddFakeDbSet<Subscriber, SubscriberDbSet>();
        }
    }
}
