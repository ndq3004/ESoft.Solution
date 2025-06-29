namespace ESoft.CRM.Domain.Interfaces.IRepository
{
    public interface IReadRepository<T> where T : class
    {
        Task<T?> GetAsync(Guid id);
        Task<T?> GetAsync(string id);
    }
}
