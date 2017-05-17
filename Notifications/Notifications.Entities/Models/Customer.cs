
using System;
using Repository.Pattern.Ef6;
using System.Collections.Generic;

namespace Notifications.Entities.Models
{
    public partial class Customer : Entity
    {
        public Customer()
        {
            this.Orders = new List<Order>();
            //this.CustomerDemographics = new List<CustomerDemographic>();
        }

        public int CustomerID { get; set; }
        public int CompanyID { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string StreetAddress_Line1 { get; set; }
        public string StreetAddress_Line2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public virtual Company Company { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        //public virtual ICollection<CustomerDemographic> CustomerDemographics { get; set; }
    }
}
