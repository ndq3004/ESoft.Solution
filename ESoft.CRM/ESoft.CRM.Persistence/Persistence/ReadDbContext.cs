using ESoft.CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESoft.CRM.Infrastructure.Persistence
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // You can use modelBuilder.ApplyConfigurationsFromAssembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadDbContext).Assembly);

            // Other entity configuration
        }
    }
}
