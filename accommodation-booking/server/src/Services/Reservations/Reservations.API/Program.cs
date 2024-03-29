using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Reservations.API.Infrasructure;
using ReservationsLibrary.Data;
using ReservationsLibrary.Services;
using Reservations.API.DTO;
using Microsoft.AspNetCore.Authentication;
using Reservations.API.Security;
using Reservations.API.Infrasructure.Persistence.Repositories;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using Reservations.API.Infrastructure.GrpcServices;
using NATS.Client;
using EventBus.NET.Integration.SubscriptionManager;
using EventBus.NET.Integration.EventBus;
using EventBus.NET.Integration.Extensions;
using ReservationsLibrary.IntegrationEvents;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using Prometheus;
using Reservations.API.Middleware;

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReservationAPI", Version = "v1" });
});

builder.Services.AddTransient<HttpRequestMetricsMiddleware>();

builder.Services.AddAuthentication("Default")
                .AddScheme<AuthenticationSchemeOptions, AuthHandler>("Default", null);
builder.Services.AddAuthorization();

builder.Services.AddDbContext<ReservationsDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Reservations")));
builder.Services.AddScoped(typeof(IAccommodationRepository), typeof(AccommodationRepository));
builder.Services.AddScoped(typeof(IReservationRepository), typeof(ReservationRepository));
builder.Services.AddScoped(typeof(IReservationRequestRepository), typeof(ReservationRequestRepository));

builder.Services.AddScoped(typeof(IAccommodationService), typeof(AccommodationService));
builder.Services.AddScoped(typeof(IReservationRequestService), typeof(ReservationRequestService));
builder.Services.AddScoped(typeof(IReservationService), typeof(ReservationService));

builder.Services.AddScoped(typeof(IAccommodationSearchGrpcService), typeof(AccommodationSearchGrpcService));

builder.Services.AddSingleton<IEventBus, NatsEventBus>();


builder.Services.AddScoped<IIdentityAPIClient, IdentityAPIClient>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddGrpcClient<GrpcAccommodationSearch.AccommodationSearch.AccommodationSearchClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:13001");
});

builder.WebHost.UseKestrel(options => {
    options.Listen(IPAddress.Any, 80, listenOptions => {
        listenOptions.Protocols = HttpProtocols.Http1;
    });

    options.Listen(IPAddress.Any, 5000, listenOptions => {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddSingleton<ISubscriptionManager, SubscriptionManager>();
builder.Services.AddSingleton<IConnection>(provider =>
{
    var factory = new ConnectionFactory();
    var url = builder.Configuration.GetSection("NATS").GetValue<string>("Url");
    return factory.CreateConnection(url);
});
builder.Services.AddSingleton<IEventBus, NatsEventBus>();
builder.Services.AddIntegrationEventsHandlers(typeof(DeleteHostRequestIntegrationEventHandler).Assembly);

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.AddHandlers(typeof(DeleteHostRequestIntegrationEventHandler).Assembly);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMetricServer();
app.UseHttpMetrics();

// app.UseMiddleware<HttpRequestMetricsMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<ReservationsGrpcService>();

app.Run();
