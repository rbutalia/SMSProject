using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Repository.Models
{
    public class CustomerOrder
    {
        public int CustomerId { get; set; }
        public string ContactName { get; set; }
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
