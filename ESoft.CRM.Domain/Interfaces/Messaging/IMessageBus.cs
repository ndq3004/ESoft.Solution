using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESoft.CRM.Domain.Interfaces.Messaging
{
    public interface IMessageBus
    {
        Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken) where T : class;
    }
}
