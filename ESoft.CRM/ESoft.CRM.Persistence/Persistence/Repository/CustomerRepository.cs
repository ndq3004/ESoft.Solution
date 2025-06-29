using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESoft.CRM.Infrastructure.Persistence.Repository
{
    public class CustomerRepository : WriteRepository<Customer>, ICustomerRepository
    {
        private readonly WriteDbContext _appContext;

        public CustomerRepository(WriteDbContext appContext) : base(appContext)
        {
            //_appContext = appContext;
        }

        public async Task DoOtherStuff(Customer customer)
        {
            // left outer join
            await Task.Run(async () =>
            {
                var customers = new List<Customer>();
                var products = new List<Product>();
                var res = from c in customers
                          join p in products
                          on c.Id equals p.CustomerId into customerProducts
                          from p in customerProducts.DefaultIfEmpty()
                          select new CustomerProduct
                          {
                              CustomerId = c.Id,
                              CustomerName = c.Name,
                              ProductId = p?.Id,
                              ProductName = p?.ProductName
                          };

                res.ToList();
                await Task.CompletedTask;
            });
        }

        class CustomerProduct
        {
            public Guid CustomerId { get; set; }
            public string CustomerName { get; set; }
            public Guid? ProductId { get; set; }
            public string? ProductName { get; set; }
        }
    }
}
