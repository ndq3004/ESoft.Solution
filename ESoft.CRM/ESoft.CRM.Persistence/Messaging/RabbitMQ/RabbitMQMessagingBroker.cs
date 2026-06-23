using ESoft.CRM.Domain.Interfaces.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESoft.CRM.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMQMessagingBroker : IMessageBus
    {
        private readonly ConnectionFactory _factory;
        public RabbitMQMessagingBroker(string hostName, string userName, string password) 
        {
            _factory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
        }
        public async Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken) where T : class
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            await channel.BasicPublishAsync(
                exchange: "", 
                routingKey: typeof(T).Name,
                body: body);
        }
    }
}
