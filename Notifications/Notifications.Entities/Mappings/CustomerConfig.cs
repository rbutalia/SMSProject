
using Notifications.Entities.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notifications.Entities.Mappings
{
    public class CustomerConfig : EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            // Primary Key
            this.HasKey(t => t.CustomerID);

            // Properties

            this.Property(t => t.CustomerID)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContactName)
                .HasMaxLength(30);

            this.Property(t => t.ContactTitle)
                .HasMaxLength(30);

            this.Property(t => t.StreetAddress_Line1)
                .HasMaxLength(60);

            this.Property(t => t.StreetAddress_Line2)
                .HasMaxLength(60);

            this.Property(t => t.City)
                .HasMaxLength(15);

            this.Property(t => t.Region)
                .HasMaxLength(15);

            this.Property(t => t.PostalCode)
                .HasMaxLength(10);

            this.Property(t => t.Country)
                .HasMaxLength(15);

            this.Property(t => t.Phone)
                .HasMaxLength(24);

            this.Property(t => t.Fax)
                .HasMaxLength(24);

            // Table & Column Mappings
            this.ToTable("Customers");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID").IsRequired();
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.ContactName).HasColumnName("ContactName");
            this.Property(t => t.ContactTitle).HasColumnName("ContactTitle");
            this.Property(t => t.StreetAddress_Line1).HasColumnName("Address_Line1");
            this.Property(t => t.StreetAddress_Line2).HasColumnName("Address_Line2");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Region).HasColumnName("Region");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Fax).HasColumnName("Fax");
        }
    }
}
