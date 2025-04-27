using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESoft.CRM.Domain.Entities;

namespace ESoft.CRM.Domain.Interfaces.CRM
{
    public interface ICRMServiceClient
    {
        Task<Customer> RegisterCustomerAsync(Customer customer);
    }
}
