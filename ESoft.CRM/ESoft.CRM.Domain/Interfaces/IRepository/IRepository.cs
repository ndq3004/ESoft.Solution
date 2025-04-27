namespace ESoft.CRM.Domain.Interfaces.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<int> SaveChangeAsync();
    }
}
