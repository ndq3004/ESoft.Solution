using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESoft.CRM.Infrastructure.Persistence.Configurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.Customer>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Customer> builder)
        {
            // Configure the Customer entity properties and relationships here
            builder.ToTable("Customers");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.HasMany(c => c.Products)
                   .WithOne(c => c.Customer)
                   .HasForeignKey(p => p.CustomerId) // Assuming Product has a CustomerId property
                   .OnDelete(DeleteBehavior.Cascade);
                   //.UsingEntity(j => j.ToTable("CustomerProducts")); // just for simple join table, and many-to-many relationships

            // Add any additional configurations as needed
        }
    }
}
