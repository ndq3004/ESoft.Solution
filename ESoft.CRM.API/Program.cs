using Esoft.Core;
using ESoft.CRM.API;
using ESoft.CRM.Application.Handlers;
using ESoft.CRM.Domain.Interfaces.CRM;
using ESoft.CRM.Domain.Interfaces.IRepository;
using ESoft.CRM.Domain.Interfaces.Messaging;
using ESoft.CRM.Infrastructure.ExternalClients;
using ESoft.CRM.Infrastructure.Messaging;
using ESoft.CRM.Infrastructure.Persistence;
using ESoft.CRM.Infrastructure.Persistence.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ESoft.CRM.Domain.Interfaces.IGraph;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Common
builder.Logging.ConfigureExtentionsService();
builder.Services.ConfigurePerService();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMassTransit(config => {
    // configure host
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.ConfigureEndpoints(ctx);
        cfg.Host("host-name", h =>
        {
            h.Username("");
            h.Password("");
        });

        // If this is consumer or bg service
        //cfg.ReceiveEndpoint("new-registered-customer", h => h.ConfigureConsumer<Handler>(ctx);
    });
});

// Add common

// Add independent services
builder.Services.AddSingleton<IEsoftGraphServiceClient, EsoftGraphServiceClient>();
builder.Services.AddSingleton<IGraphClient, ESoftGraphQLHttpClient>();

builder.Services.AddSingleton<ICRMServiceClient, CRMServiceClient>();
builder.Services.AddSingleton<IPIMServiceClient, PIMServiceClient>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IMessageBus, MassTransitMessagingBroker>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
