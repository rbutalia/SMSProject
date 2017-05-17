
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
            this.HasKey(t => new { t.OrderID, t.MenuItemID });

            // Properties
            this.Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MenuItemID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("OrderDetails");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.MenuItemID).HasColumnName("MenuItemID");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Discount).HasColumnName("Discount");

            // Relationships
            this.HasRequired(t => t.Order)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(d => d.OrderID);
            this.HasRequired(t => t.Dish)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(d => d.MenuItemID);

        }
    }
}
