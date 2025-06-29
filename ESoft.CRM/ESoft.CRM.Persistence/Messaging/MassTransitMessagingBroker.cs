using ESoft.CRM.Domain.Interfaces.Messaging;
using MassTransit;

namespace ESoft.CRM.Infrastructure.Messaging
{
    public class MassTransitMessagingBroker : IMessageBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public MassTransitMessagingBroker(IPublishEndpoint publishEndpoint) 
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken) where T : class
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
