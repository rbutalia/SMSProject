using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Entities.Models
{
    public class Subscriber: Entity
    {
        public int SubscriberID { get; set; }
        public int CustomerId { get; set; }
        public String PhoneNumber { get; set; }
        public bool Subscribed { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
