using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESoft.CRM.Infrastructure.Persistence.Repository
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly WriteDbContext _appContext;
        private readonly DbSet<T> _entities;

        public WriteRepository(WriteDbContext appContext)
        {
            _appContext = appContext;
            _entities = _appContext.Set<T>();
        }

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
