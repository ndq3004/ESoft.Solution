using ESoft.CRM.Domain.Entities;

namespace ESoft.CRM.Domain.Interfaces.Messaging
{
    public interface IMessageBus
    {
        Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken) where T : class;
    }

    public interface IDirectMessageBus
    {
        Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken) where T : BaseEntity;
    }
}
