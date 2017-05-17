
using Repository.Pattern.Ef6;

namespace Notifications.Entities.Models
{
    public partial class OrderDetail : Entity
    {
        public int OrderID { get; set; }
        public int MenuItemID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public virtual Order Order { get; set; }
        public virtual MenuItem Dish { get; set; }
    }
}

