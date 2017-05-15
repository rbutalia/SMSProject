
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
    }
}