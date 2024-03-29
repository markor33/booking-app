 using EventBus.NET.Integration.EventBus;
using EventBus.NET.Integration.Extensions;
using EventBus.NET.Integration.SubscriptionManager;
using Microsoft.AspNetCore.Authentication;
using NATS.Client;
using Notifications.SignalR.Hubs;
using Notifications.SignalR.IntegrationEvents.Notifications;
using Notifications.SignalR.Middleware;
using Notifications.SignalR.Persistence.Repositories;
using Notifications.SignalR.Persistence.Settings;
using Notifications.SignalR.Security;
using Notifications.SignalR.Services;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using Reservations.API.Security;

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

builder.Services.AddTransient<HttpRequestMetricsMiddleware>();

builder.Services.AddScoped<IIdentityAPIClient, IdentityAPIClient>();

builder.Services.AddAuthentication("Default")
    .AddScheme<AuthenticationSchemeOptions, AuthHandler>("Default", null);
builder.Services.AddAuthorization();

var mongoDbSettings = builder.Configuration.GetSection("MongoDB");
builder.Services.Configure<MongoDBSettings>(mongoDbSettings);
builder.Services.AddScoped(typeof(IMongoDbFactory), typeof(MongoDbFactory));

builder.Services.AddScoped(typeof(INotificationRepository), typeof(NotificationRepository));
builder.Services.AddScoped(typeof(IUserNotificationsConfigRepository), typeof(UserNotificationsConfigRepository));

builder.Services.AddScoped(typeof(INotificationService), typeof(NotificationService));

builder.Services.AddSingleton<ISubscriptionManager, SubscriptionManager>();
builder.Services.AddSingleton<IConnection>(provider =>
{
    var factory = new ConnectionFactory();
    var url = builder.Configuration.GetSection("NATS").GetValue<string>("Url");
    return factory.CreateConnection(url);
});
builder.Services.AddSingleton<IEventBus, NatsEventBus>();
builder.Services.AddIntegrationEventsHandlers(typeof(ReservationRequestCreatedNotification).Assembly);

builder.Services.AddSignalR();
builder.Services.AddSingleton(typeof(IActiveUserService), typeof(ActiveUserService));

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.AddHandlers(typeof(ReservationRequestCreatedNotification).Assembly);

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

app.MapHub<NotificationsHub>("/hub/notifications");

app.MapControllers();

app.Run();
