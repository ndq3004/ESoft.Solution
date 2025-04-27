using System.Reflection;
using ESoft.CRM.Application.Handlers;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using MediatR;

namespace ESoft.CRM.API
{
    public static class HostingExtentions
    {
        public static void ConfigurePerService(this IServiceCollection services)
        {
            //TODO: setup polly for retry policy
            services.AddHttpClient(
                "CRMSystem",
                client =>
                {
                    client.BaseAddress = new Uri("http://example-crm.com");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddHttpClient(
                "PIMSystem",
                client =>
                {
                    client.BaseAddress = new Uri("http://example-pim.com");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            //Config mediatr
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductByIdHandler).GetTypeInfo().Assembly));
            // services.AddMediatR(typeof(GetProductByIdHandler).GetTypeInfo().Assembly); //TODO: in tutorial, it should be GetTypeInfo()

            //Configure AutoMapper
            services.AddAutoMapper(typeof(Program));

            // Add graphql client
            services.AddHttpClient<GraphQLHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://mygraphclient.com/graphql");
                // Add header
            })
            .AddTypedClient(client => new GraphQLHttpClient(new GraphQLHttpClientOptions
            {
                EndPoint = client.BaseAddress
            }, new NewtonsoftJsonSerializer(), client));

        }
    }
}
