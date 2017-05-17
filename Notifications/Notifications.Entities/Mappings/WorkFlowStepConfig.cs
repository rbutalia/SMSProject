
using Notifications.Entities.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notifications.Entities.Mappings
{
    public class WorkflowStepConfig: EntityTypeConfiguration<WorkflowStep>
    {
        public WorkflowStepConfig()
        {
            // Primary Key
            this.HasKey(t => t.WorkflowStepID);

            // Properties
            this.Property(t => t.WorkflowStepID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            this.Property(t => t.CompanyID).IsRequired();

            this.Property(t => t.StepName)
                .HasMaxLength(20);

            this.Property(t => t.Description)
                .HasMaxLength(30);

            this.Property(t => t.RegularExpression)
                .HasMaxLength(30);

            this.ToTable("WorkflowSteps");
            this.Property(t => t.WorkflowStepID).HasColumnName("WorkflowStepID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID").IsRequired();
            this.Property(t => t.StepName).HasColumnName("StepName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.RegularExpression).HasColumnName("RegularExpression").IsRequired();
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.HasRequired(t => t.Company)
                .WithMany(t => t.WorkFlowSteps)
                .HasForeignKey(d => d.CompanyID).WillCascadeOnDelete(true);
        }
    }
}
