
using Notifications.Entities.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notifications.Entities.Mappings
{
    public class MenuConfig: EntityTypeConfiguration<Menu>
    {
        public MenuConfig()
        {
            this.HasKey(t => t.MenuID);

            // Properties

            this.Property(t => t.MenuID)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(30);

            this.Property(t => t.ModifiedBy)
               .HasMaxLength(30);

            this.ToTable("Menus");
            this.Property(t => t.MenuID).HasColumnName("MenuID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");


        }
    }
}
