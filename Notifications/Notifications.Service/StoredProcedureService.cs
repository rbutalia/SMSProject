
using System.Collections.Generic;
using Notifications.Entities.StoredProcedures;
using Notifications.Entities.Models.StoredProcedures;

namespace Notifications.Services
{
    public interface IStoredProcedureService
    {
        IEnumerable<CustomerOrderHistory> CustomerOrderHistory(string customerID);
        int CustOrdersDetail(int? orderID);
        IEnumerable<CustomerOrderDetail> CustomerOrderDetail(string customerID);
    }
    public class StoredProcedureService: IStoredProcedureService
    {
        private readonly INotificationStoredProcedures _storedProcedures;

        public StoredProcedureService(INotificationStoredProcedures storedProcedures)
        {
            _storedProcedures = storedProcedures;
        }

        public IEnumerable<CustomerOrderHistory> CustomerOrderHistory(string customerID)
        {
            return _storedProcedures.CustomerOrderHistory(customerID);
        }

        public int CustOrdersDetail(int? orderID)
        {
            return _storedProcedures.CustOrdersDetail(orderID);
        }

        public IEnumerable<CustomerOrderDetail> CustomerOrderDetail(string customerID)
        {
            return _storedProcedures.CustomerOrderDetail(customerID);
        }
    }
}
