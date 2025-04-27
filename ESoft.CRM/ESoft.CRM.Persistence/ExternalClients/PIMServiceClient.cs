using System.Text.Json;
using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Domain.Interfaces.CRM;

namespace ESoft.CRM.Infrastructure.ExternalClients
{
    public class PIMServiceClient : IPIMServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PIMServiceClient(IHttpClientFactory httpClientFactory) 
        { 
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            using var httpClient = _httpClientFactory.CreateClient("PIMSystem");

            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(string.Format("/product?id={0}", productId));
                httpResponse.EnsureSuccessStatusCode();
                var responseBody = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Product>(responseBody);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
