using ESoft.CRM.Application.Queries;
using ESoft.CRM.Domain.Entities;
using ESoft.Core.Interfaces.Logging;
using ESoft.CRM.Domain.Interfaces.IGraph;
using MediatR;
using ESoft.CRM.Application.Commands;
using ESoft.CRM.Application.Mappers;
using ESoft.CRM.Domain.Interfaces.CRM;
using ESoft.CRM.Application.Events;
using ESoft.CRM.Domain.Interfaces.IRepository;
using MassTransit;
using ESoft.CRM.Domain.Interfaces.Messaging;
using Microsoft.Extensions.Logging;

namespace ESoft.CRM.Application.Handlers
{
    public class RegisterCustomerHandler : IRequestHandler<RegisterCustomerCommand, Customer>
    {
        private readonly ILogger<RegisterCustomerHandler> _log;
        private readonly ICRMServiceClient _crmService;
        private readonly IMessageBus _messageBus;

        // Or persistence in SQL database
        private readonly ICustomerRepository _customerRepository;
        public RegisterCustomerHandler(ILogger<RegisterCustomerHandler> log,
            ICRMServiceClient crmService,
            IMessageBus messageBus,
            ICustomerRepository customerRepository
            )
        {
            _log = log;
            _crmService = crmService;
            _messageBus = messageBus;
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            _log.LogInformation($"Command received: {nameof(RegisterCustomerCommand)} | ObjectName: {request.Name}");
            var crmCustomer = CRMMapper.Mapper.Map<Customer>(request);

            if (crmCustomer is null) 
            {
                throw new ApplicationException("Error when create crm customer");   
            }

            #region Register with external system

            var result = await _crmService.RegisterCustomerAsync(crmCustomer);
            if (result is null)
            {
                throw new ApplicationException("Failed to register new customer");
            }

            #endregion

            //Or

            #region Register to database

            result = await _customerRepository.AddAsync(result);
            int success = await _customerRepository.SaveChangeAsync();

            if (success < 1)
            {
                throw new ApplicationException();
            }
            #endregion


            //TODO: create BG service to handle with a handler IConsume<NewCustomerCreatedEvent>
            await _messageBus.PublishMessageAsync<NewCustomerCreatedEvent>(new NewCustomerCreatedEvent
            {
                CustomerId = result.Id
            }, cancellationToken);

            _log.LogInformation($"Command {nameof(NewCustomerCreatedEvent)} sent");

            return result;
        }
    }
}
