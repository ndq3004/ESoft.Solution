using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESoft.CRM.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ESoft.CRM.Infrastructure.Persistence
{
    public class DbConnectionStringProvider : IDbConnectionStringProvider
    {
        private readonly IConfiguration Configuration;
        public DbConnectionStringProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string GetConnectionString()
        {
            // Implement logic to retrieve the default connection string
            return "DefaultConnectionString";
        }
        public string GetConnectionString(string connectionName)
        {
            // Implement logic to retrieve the connection string by name
            return $"ConnectionStringFor_{connectionName}";
        }
        // Other methods can be implemented as needed

    }
}
