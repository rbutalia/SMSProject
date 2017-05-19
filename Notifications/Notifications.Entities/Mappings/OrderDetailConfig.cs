
using Notifications.Entities.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notifications.Entities.Mappings
{
    public class OrderDetailConfig : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfig()
        {
            // Primary Key
            //this.HasKey(t => new { t.OrderID, t.MenuItemID });
            this.HasKey(t => t.OrderDetailID);

            this.Property(t => t.OrderDetailID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            //this.Property(t => t.OrderID)
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //this.Property(t => t.MenuItemID)
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("OrderDetails");
            this.Property(t => t.OrderDetailID).HasColumnName("OrderDetailID");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.MenuItemID).HasColumnName("MenuItemID");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Order)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(d => d.OrderID).WillCascadeOnDelete(false);
            this.HasRequired(t => t.Dish)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(d => d.MenuItemID);

        }
    }
}
