using ESoft.CRM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ESoft.CRM.Infrastructure.Persistence
{
    public class ReadDbContextFactory : IDbContextFactory<ReadDbContext>
    {
        private ReadDbContext _dbContext;
        private readonly IDbConnectionStringProvider _connectionStringProvider;
        public ReadDbContextFactory()
        {

        }

        public ReadDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReadDbContext>();
            optionsBuilder.UseSqlServer(_connectionStringProvider.GetConnectionString("ReadDB"));

            // in case same scope/request, no need to re-create context
            if (_dbContext == null)
            {
                _dbContext = new ReadDbContext(optionsBuilder.Options);
            }
            return _dbContext;
        }

    }
}
