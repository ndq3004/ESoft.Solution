using ESoft.CRM.Application.Queries;
using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Domain.Interfaces.IGraph;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ESoft.CRM.Application.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, InternalAdUser?>
    {
        private readonly ILogger<GetUserByIdHandler> _log;
        private readonly IEsoftGraphServiceClient _esoftGraphClient;
        public GetUserByIdHandler(ILogger<GetUserByIdHandler> log,
            IEsoftGraphServiceClient esoftGraphClient)
        {
            _log = log;
            _esoftGraphClient = esoftGraphClient;
        }
        public async Task<InternalAdUser?> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _esoftGraphClient.GetClient().GetUserById(request.UserId, _log, request.ThrowOnNotFound);
            return result;
        }
    }
}
