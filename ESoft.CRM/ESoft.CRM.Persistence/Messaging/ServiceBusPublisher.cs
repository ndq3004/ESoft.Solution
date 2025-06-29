using ESoft.CRM.Domain.Interfaces.Messaging;
using Azure.Messaging.ServiceBus;
using ESoft.CRM.Domain.Entities;
using System.Security.Cryptography;

namespace ESoft.CRM.Infrastructure.Messaging
{
    public class ServiceBusPublisher : IDirectMessageBus
    {
        private readonly ServiceBusClient _serviceBusClient;
        private const int PartitionCount = 10;
        private SHA256 _sha = SHA256.Create();
        public ServiceBusPublisher(ServiceBusClient serviceBusClient) 
        {
            _serviceBusClient = serviceBusClient;
        }
        public async Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken) where T : BaseEntity
        {
            var sender = _serviceBusClient.CreateSender(typeof(T).Name);

            using ServiceBusMessageBatch serviceBusMessageBatch = await sender.CreateMessageBatchAsync(cancellationToken);
            for(int i = 0; i < 100; i++) {
                message.Id = Guid.NewGuid(); // Ensure each message has a unique ID for partitioning
                byte[] hashBytes = _sha.ComputeHash(message.Id.ToByteArray());
                int hashInt = BitConverter.ToInt32(hashBytes, 0);
                int partitionKey = Math.Abs(hashInt % PartitionCount);

                string serializedMessage = System.Text.Json.JsonSerializer.Serialize(message);

                if (!serviceBusMessageBatch.TryAddMessage(new ServiceBusMessage(serializedMessage)
                {
                    PartitionKey = $"partition-{partitionKey}"
                    //TransactionPartitionKey = typeof(T).Name //TODO: ??
                }))
                {
                    throw new Exception("The message is too large to fit in the batch.");
                }
            }

            await sender.SendMessagesAsync(serviceBusMessageBatch, cancellationToken);

            //await sender.SendMessageAsync(new ServiceBusMessage(serializedMessage)
            //{
            //    PartitionKey = $"partition-{partitionKey}"
            //    //TransactionPartitionKey = typeof(T).Name //TODO: ??
            //}, cancellationToken);
            await sender.DisposeAsync();

        }
    }
}
