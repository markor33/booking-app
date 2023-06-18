using EventBus.NET.Integration.EventBus;
using EventBus.NET.Integration.Extensions;
using EventBus.NET.Integration.SubscriptionManager;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using NATS.Client;
using Ratings.API.Infrastructure.GrpcServices;
using Ratings.API.Infrastructure.Persistence;
using Ratings.API.Infrastructure.Persistence.Repositories;
using Ratings.API.Security;
using RatingsLibrary.BackgroundTasks;
using RatingsLibrary.IntegrationEvents;
using RatingsLibrary.Repository;
using RatingsLibrary.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Default")
                .AddScheme<AuthenticationSchemeOptions, AuthHandler>("Default", null);
builder.Services.AddAuthorization();

builder.Services.AddDbContext<RatingsDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Ratings")));

builder.Services.AddScoped(typeof(IAccommodationRatingRepository), typeof(AccommodationRatingRepository));
builder.Services.AddScoped(typeof(IReservationRepository), typeof(ReservationRepository));
builder.Services.AddScoped(typeof(IHostRatingRepository), typeof(HostRatingRepository));
builder.Services.AddScoped(typeof(IProminentHostRepository), typeof(ProminentHostRepository));

builder.Services.AddScoped(typeof(IAccommodationRatingService), typeof(AccommodationRatingService));
builder.Services.AddScoped(typeof(IReservationService), typeof(ReservationService));
builder.Services.AddScoped(typeof(IHostRatingService), typeof(HostRatingService));
builder.Services.AddScoped(typeof(IProminentHostService), typeof(ProminentHostService));

builder.Services.AddHostedService<HostStatusCheckTask>();

builder.Services.AddScoped<IIdentityAPIClient, IdentityAPIClient>();

builder.Services.AddSingleton<ISubscriptionManager, SubscriptionManager>();
builder.Services.AddSingleton<IConnection>(provider =>
{
    var factory = new ConnectionFactory();
    var url = builder.Configuration.GetSection("NATS").GetValue<string>("Url");
    return factory.CreateConnection(url);
});

builder.Services.AddSingleton<IEventBus, NatsEventBus>();
builder.Services.AddIntegrationEventsHandlers(typeof(ReservationCanceledIntegrationEventHandler).Assembly);

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});

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
eventBus.AddHandlers(typeof(ReservationCanceledIntegrationEventHandler).Assembly);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<RatingsGrpcService>();

app.Run();
