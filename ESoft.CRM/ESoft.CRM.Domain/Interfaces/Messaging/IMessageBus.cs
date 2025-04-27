namespace ESoft.CRM.Domain.Interfaces.Messaging
{
    public interface IMessageBus
    {
        Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken) where T : class;
    }
}
