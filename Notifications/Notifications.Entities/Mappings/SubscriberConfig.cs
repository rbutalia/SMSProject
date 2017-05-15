
using Notifications.Entities.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notifications.Entities.Mappings
{
    public class SubscriberConfig : EntityTypeConfiguration<Subscriber>
    {
        public SubscriberConfig()
        {
            // Primary Key
            this.HasKey(t => t.SubscriberID);

            // Properties

            this.Property(t => t.SubscriberID)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Subscribers");
            this.Property(t => t.SubscriberID).HasColumnName("SubscriberID");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId").IsRequired();
            this.Property(t => t.Subscribed).HasColumnName("Subscribed");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
