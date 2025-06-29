using ESoft.CRM.Domain.Entities;

namespace ESoft.CRM.Domain.Interfaces.IRepository
{
    public interface ICustomerRepository : IWriteRepository<Customer>
    {
        Task DoOtherStuff(Customer customer);
    }
}
