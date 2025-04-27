using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESoft.CRM.Infrastructure.Persistence.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _appContext;

        public CustomerRepository(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public Task DoOtherStuff(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
