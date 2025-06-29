namespace ESoft.CRM.Domain.Interfaces.IRepository
{
    public interface IWriteRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<int> SaveChangeAsync();
    }
}
