using MediatR;
using Microsoft.AspNetCore.Mvc;
using ESoft.CRM.Application.Queries;
using ESoft.CRM.Domain.Entities;

namespace ESoft.CRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<InternalAdUser?>> Get(Guid productId, string traceId, CancellationToken cancellationToken)
        {
            var adUser = await _mediator.Send(new GetProductByIdRequest(productId, traceId), cancellationToken);
            return Ok(adUser);
        }
    }
}
