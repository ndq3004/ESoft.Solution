using ESoft.CRM.Domain.Entities;
using MediatR;

namespace ESoft.CRM.Application.Queries
{
    public record GetUserByIdRequest(Guid UserId, string TraceId, bool ThrowOnNotFound = false) : IRequest<InternalAdUser?>;
}
