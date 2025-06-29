using ESoft.CRM.Application.Queries;
using ESoft.CRM.Domain.Entities;
using MediatR;
using ESoft.CRM.Domain.Interfaces.CRM;
using Microsoft.Extensions.Logging;

namespace ESoft.CRM.Application.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, Product?>
    {
        private readonly ILogger<GetProductByIdHandler> _log;
        private readonly IPIMServiceClient _pimService;
        public GetProductByIdHandler(ILogger<GetProductByIdHandler> log,
            IPIMServiceClient pimService)
        {
            _log = log;
            _pimService = pimService;
        }

        /// <summary>
        /// Get Product Information (in case getting from PIMSystem)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Product?> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            _log.LogInformation($"Start query {nameof(GetProductByIdRequest)} | TraceId: {request.TraceId}");
            var result = await _pimService.GetProductByIdAsync(request.productId);

            return result;
        }
    }
}
