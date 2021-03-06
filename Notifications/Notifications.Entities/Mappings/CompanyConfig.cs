﻿
using Notifications.Entities.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notifications.Entities.Mappings
{
    public class CompanyConfig : EntityTypeConfiguration<Company>
    {
        public CompanyConfig()
        {
            // Primary Key
            this.HasKey(t => t.CompanyID);

            // Properties
            this.Property(t => t.CompanyID)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CompanyName)
                .HasMaxLength(30);

            this.Property(t => t.TextIdentifier)
                .HasMaxLength(8);

            this.Property(t => t.ContactPersonName)
                .HasMaxLength(30);

            this.Property(t => t.SalesTax).HasPrecision(4, 2);

            this.ToTable("Companies");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.TextIdentifier).HasColumnName("TextIdentifier");
            this.Property(t => t.ContactPersonName).HasColumnName("ContactPersonName");
            this.Property(t => t.SalesTax).HasColumnName("SalesTax");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
