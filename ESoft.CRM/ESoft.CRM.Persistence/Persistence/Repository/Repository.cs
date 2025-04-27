using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESoft.CRM.Infrastructure.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _appContext;
        private readonly DbSet<T> _entities;
        public async Task<T> AddAsync(T entity)
        {
            entity.Id = Guid.NewGuid();
            await _entities.AddAsync(entity);
            return entity;
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _appContext.SaveChangesAsync();
        }
    }
}
