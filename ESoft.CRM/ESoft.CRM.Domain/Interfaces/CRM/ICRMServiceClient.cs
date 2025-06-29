using ESoft.CRM.Domain.Entities;

namespace ESoft.CRM.Domain.Interfaces.CRM
{
    public interface ICRMServiceClient
    {
        Task<Customer> RegisterCustomerAsync(Customer customer);
    }
}
