
using System;

namespace Notifications.Entities.Models.StoredProcedures
{
    
    public class CustomerOrderDetail
    {
        public int OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
    }
}
