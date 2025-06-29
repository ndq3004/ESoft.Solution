using ESoft.CRM.Domain.Entities;

namespace ESoft.CRM.Domain.Interfaces.CRM
{
    public interface IPIMServiceClient
    {
        Task<Product> GetProductByIdAsync(Guid productId);
    }
}
