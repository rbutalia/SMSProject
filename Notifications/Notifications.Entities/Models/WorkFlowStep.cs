
using System;
using Repository.Pattern.Ef6;

namespace Notifications.Entities.Models
{
    public partial class WorkflowStep: Entity
    {
        public int WorkflowStepID { get; set; }
        public int CompanyID { get; set; }
        public string StepName { get; set; }
        public string Description { get; set; }
        public string RegularExpression { get; set; }
        public virtual Company Company { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
