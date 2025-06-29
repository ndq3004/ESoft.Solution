using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Domain.Interfaces.Messaging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESoft.CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDirectMessageBus _directMessageBus;
        public PaymentsController(IMediator mediator, IDirectMessageBus directMessageBus)
        {
            _mediator = mediator;
            _directMessageBus = directMessageBus;
        }
        [HttpGet]
        public async Task Create()
        {
            await _directMessageBus.PublishMessageAsync(new Payment
            {
                Id = Guid.NewGuid(),
                Description = "Payment for services rendered",
                Amount = 100.00m,
                PaymentDate = DateTime.UtcNow
            }, CancellationToken.None);
        }

        public class Payment : BaseEntity
        {
            public string Description { get; set; }
            public decimal Amount { get; set; }
            public DateTime PaymentDate { get; set; }
        }
    }
}
