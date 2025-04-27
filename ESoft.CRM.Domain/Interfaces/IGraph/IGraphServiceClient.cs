using ESoft.CRM.Domain.Entities;
using ESoft.Core.Interfaces.Logging;
using Microsoft.Extensions.Logging;

namespace ESoft.CRM.Domain.Interfaces.IGraph
{
    public interface IEsoftGraphServiceClient
    {
        IGraphClient GetClient();
    }
    public interface IGraphClient
    {
        Task<InternalAdUser?> GetUserById(Guid userId, ILogger log, bool throwOnNotFound);
    }
}
