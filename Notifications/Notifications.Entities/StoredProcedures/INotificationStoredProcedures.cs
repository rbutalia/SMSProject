
using System;
using System.Collections.Generic;
using Notifications.Entities.Models.StoredProcedures;

namespace Notifications.Entities.StoredProcedures
{
    public interface INotificationStoredProcedures
    {
        IEnumerable<CustomerOrderHistory> CustomerOrderHistory(string customerID);
        int CustOrdersDetail(int? orderID);
        IEnumerable<CustomerOrderDetail> CustomerOrderDetail(string customerID);
        int EmployeeSalesByCountry(DateTime? beginningDate, DateTime? endingDate);
        int SalesByCategory(string categoryName, string ordYear);
        int SalesByYear(DateTime? beginningDate, DateTime? endingDate);
    }
}