
using Notifications.Entities.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notifications.Entities.Mappings
{
    public class MenuItemConfig : EntityTypeConfiguration<MenuItem>
    {
        public MenuItemConfig()
        {
            this.HasKey(t => t.MenuItemID);

            // Properties

            this.Property(t => t.MenuItemID)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ItemName)
                .HasMaxLength(30);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(30);

            this.Property(t => t.ModifiedBy)
               .HasMaxLength(30);

            this.ToTable("MenuItems");
            this.Property(t => t.MenuItemID).HasColumnName("MenuItemID");
            this.Property(t => t.MenuID).HasColumnName("MenuID");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships

            //this.HasOptional(t => t.Menu)
            //    .WithMany(t => t.MenuItems)
            //    .HasForeignKey(d => d.MenuID);
            
        }
    }
}
