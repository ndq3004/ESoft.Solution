using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Domain.Interfaces.CRM;

namespace ESoft.CRM.Infrastructure.ExternalClients
{
    public class CRMServiceClient : ICRMServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CRMServiceClient(IHttpClientFactory httpClientFactory) 
        { 
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Customer> RegisterCustomerAsync(Customer customer)
        {
            using var httpClient = _httpClientFactory.CreateClient("CRMSystem");

            try
            {
                using StringContent json = new(
                    JsonSerializer.Serialize(customer, new JsonSerializerOptions(JsonSerializerDefaults.Web)),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json);

                HttpResponseMessage httpResponse = await httpClient.PostAsync("/customer", json);
                httpResponse.EnsureSuccessStatusCode();
                var responseBody = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Customer>(responseBody);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
