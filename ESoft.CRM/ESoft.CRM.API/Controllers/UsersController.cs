using MediatR;
using Microsoft.AspNetCore.Mvc;
using ESoft.CRM.Application.Queries;
using ESoft.CRM.Domain.Entities;

namespace ESoft.CRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<InternalAdUser?>> Get(Guid userId, string traceId, CancellationToken cancellationToken)
        {
            var adUser = await _mediator.Send(new GetUserByIdRequest(userId, traceId), cancellationToken);
            return Ok(adUser);
        }
    }
}
