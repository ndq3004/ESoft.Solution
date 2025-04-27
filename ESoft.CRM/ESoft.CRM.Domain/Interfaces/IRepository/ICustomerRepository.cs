using ESoft.CRM.Domain.Entities;

namespace ESoft.CRM.Domain.Interfaces.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task DoOtherStuff(Customer customer);
    }
}
