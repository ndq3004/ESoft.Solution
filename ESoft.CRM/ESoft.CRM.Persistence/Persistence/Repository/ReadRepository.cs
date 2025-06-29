using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESoft.CRM.Infrastructure.Persistence.Repository
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly WriteDbContext _appContext;
        private readonly DbSet<T> _entities;

        public ReadRepository(WriteDbContext appContext)
        {
            _appContext = appContext;
            _entities = _appContext.Set<T>();
        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T?> GetAsync(string id)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(e => e.Id.ToString() == id);
        }
    }
}
