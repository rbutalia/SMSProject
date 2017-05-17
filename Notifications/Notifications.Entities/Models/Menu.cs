
using System;
using Repository.Pattern.Ef6;
using System.Collections.Generic;

namespace Notifications.Entities.Models
{
    public partial class Menu: Entity
    {
        public Menu()
        {
            this.MenuItems = new List<MenuItem>();
        }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public int CompanyID { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
