
using System;
using Repository.Pattern.Ef6;
using System.Collections.Generic;

namespace Notifications.Entities.Models
{
    public partial class Company : Entity
    {
        public Company()
        {
            this.Menus = new List<Menu>();
        }
        public int CompanyID { get; set; }
        public string TextIdentifier { get; set; }
        public string CompanyName { get; set; }
        public string ContactPersonName { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}