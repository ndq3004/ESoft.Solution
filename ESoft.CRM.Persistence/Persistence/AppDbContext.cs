using ESoft.CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESoft.CRM.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // You can use modelBuilder.ApplyConfigurationsFromAssembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Other entity configuration
        }
    }
}
