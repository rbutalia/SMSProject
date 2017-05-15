
using System;
using Repository.Pattern.Ef6;
using System.Collections.Generic;


namespace Notifications.Entities.Models
{
    public partial class Order : Entity
    {
        public Order()
        {
            this.OrderDetails = new List<OrderDetail>();
        }

        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public Nullable<DateTime> ShippedDate { get; set; }
        public Nullable<int> ShipVia { get; set; }
        public Nullable<decimal> Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public virtual Customer Customer { get; set; }
       // public virtual Employee Employee { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Shipper Shipper { get; set; }
    }
}
