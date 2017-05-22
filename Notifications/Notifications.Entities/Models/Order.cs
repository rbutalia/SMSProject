
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
        public int CompanyID { get; set; }
        public int CustomerID { get; set; }
        //public int? EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public decimal Total { get; set; }
        public decimal TotalWithTax { get; set; }
        public virtual Company Company { get; set; }
        public virtual Customer Customer { get; set; }
       // public virtual Employee Employee { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        //public virtual Shipper Shipper { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
