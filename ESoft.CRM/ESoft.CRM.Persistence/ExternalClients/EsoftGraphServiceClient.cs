using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Domain.Interfaces.IGraph;
using GraphQL.Client.Http;
using Microsoft.Extensions.Logging;

namespace ESoft.CRM.Infrastructure.ExternalClients
{
    public class EsoftGraphServiceClient : IEsoftGraphServiceClient
    {
        private static IGraphClient _client;
        public EsoftGraphServiceClient(IGraphClient client)
        {
            _client = client;
        }
        public IGraphClient GetClient() => _client;
    }

    public class ESoftGraphQLHttpClient : IGraphClient
    {
        private readonly GraphQLHttpClient _client;

        public ESoftGraphQLHttpClient(GraphQLHttpClient client)
        {
            _client = client;
        }
        public async Task<InternalAdUser?> GetUserById(Guid userId, ILogger log, bool throwOnNotFound)
        {
            var response = await _client.SendQueryAsync<InternalAdUser>(new GraphQLHttpRequest());
            if (response != null) return response.Data;

            return null;
        }
    }
}
