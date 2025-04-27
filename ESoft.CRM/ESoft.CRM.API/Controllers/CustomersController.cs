using MediatR;
using Microsoft.AspNetCore.Mvc;
using ESoft.CRM.Application.Queries;
using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Application.Commands;

namespace ESoft.CRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<InternalAdUser?>> Create(RegisterCustomerCommand registerCustomerCommand, CancellationToken cancellationToken)
        {
            var adUser = await _mediator.Send(registerCustomerCommand, cancellationToken);
            return Ok(adUser);
        }
    }
}
