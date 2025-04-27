using ESoft.CRM.Domain.Entities;
using MediatR;

namespace ESoft.CRM.Application.Queries
{
    public record GetProductByIdRequest(Guid productId, string TraceId, bool ThrowOnNotFound = false) : IRequest<Product?>;
}
