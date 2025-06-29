using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESoft.CRM.Domain.Interfaces
{
    public interface IDbConnectionStringProvider
    {
        string GetConnectionString();
        string GetConnectionString(string connectionName);
        //string GetConnectionString(string connectionName, string databaseName);
        //string GetConnectionString(string connectionName, string databaseName, bool isReadOnly);
        //string GetConnectionString(string connectionName, bool isReadOnly);
        //string GetConnectionString(bool isReadOnly);
    }
}
