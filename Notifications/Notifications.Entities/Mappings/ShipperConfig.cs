
using Notifications.Entities.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notifications.Entities.Mappings
{
    public class ShipperConfig : EntityTypeConfiguration<Shipper>
    {
        public ShipperConfig()
        {
            // Primary Key
            this.HasKey(t => t.ShipperID);

            // Properties

            this.Property(t => t.ShipperID)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.Phone)
                .HasMaxLength(24);

            // Table & Column Mappings
            this.ToTable("Shippers");
            this.Property(t => t.ShipperID).HasColumnName("ShipperID");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.Phone).HasColumnName("Phone");
        }
    }
}
