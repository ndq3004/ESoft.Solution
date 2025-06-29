using ESoft.CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESoft.CRM.Infrastructure.Persistence
{
    public class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // You can use modelBuilder.ApplyConfigurationsFromAssembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);

            // Other entity configuration
        }
    }
}
