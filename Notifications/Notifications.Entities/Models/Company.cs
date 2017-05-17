
using System;
using Repository.Pattern.Ef6;

namespace Notifications.Entities.Models
{
    public partial class Company : Entity
    {
        public Company()
        {
            //this.Products = new List<Product>();
        }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ContactPersonName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}