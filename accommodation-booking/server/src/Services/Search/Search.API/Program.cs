using EventBus.NET.Integration.EventBus;
using EventBus.NET.Integration.Extensions;
using EventBus.NET.Integration.SubscriptionManager;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using NATS.Client;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Search.API.GrpcServices;
using Search.API.IntegrationEvents;
using Search.API.Persistence.Repositories;
using Search.API.Persistence.Settings;
using Search.API.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracingProviderBuilder =>
        tracingProviderBuilder
        .AddSource(builder.Environment.ApplicationName)
        .ConfigureResource(resource => resource.AddService(builder.Environment.ApplicationName))
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddEntityFrameworkCoreInstrumentation()
        .AddGrpcClientInstrumentation()
        .AddJaegerExporter(config =>
        {
            config.Endpoint = new Uri("http://host.docker.internal:14268");
            config.AgentHost = "host.docker.internal";
        }));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoDbSettings = builder.Configuration.GetSection("MongoDB");
builder.Services.Configure<MongoDBSettings>(mongoDbSettings);
builder.Services.AddScoped(typeof(IMongoDbFactory), typeof(MongoDbFactory));

builder.Services.AddScoped(typeof(IAccommodationRepository), typeof(AccommodationRepository));

builder.Services.AddScoped(typeof(ISearchService), typeof(SearchService));

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddSingleton<ISubscriptionManager, SubscriptionManager>();
builder.Services.AddSingleton<IConnection>(provider =>
{
    var factory = new ConnectionFactory();
    var url = builder.Configuration.GetSection("NATS").GetValue<string>("Url");
    return factory.CreateConnection(url);
});
builder.Services.AddSingleton<IEventBus, NatsEventBus>();
builder.Services.AddIntegrationEventsHandlers(typeof(AccommodationCreatedIntegrationEventHandler).Assembly);

builder.WebHost.UseKestrel(options => {
    options.Listen(IPAddress.Any, 80, listenOptions => {
        listenOptions.Protocols = HttpProtocols.Http1;
    });

    options.Listen(IPAddress.Any, 5000, listenOptions => {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.AddHandlers(typeof(AccommodationCreatedIntegrationEventHandler).Assembly);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<SearchGrpcService>();

app.Run();
