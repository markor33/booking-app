using Microsoft.AspNetCore.Authentication;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using Web.Bff.Middleware;
using Web.Bff.Security;
using Web.Bff.Services;

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

builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<HttpRequestMetricsMiddleware>();

builder.Services.AddScoped<IIdentityAPIClient, IdentityAPIClient>();

builder.Services.AddAuthentication("Default")
                .AddScheme<AuthenticationSchemeOptions, AuthHandler>("Default", null);
builder.Services.AddAuthorization();
builder.Services.AddOcelot();
builder.Services.AddTransient<AggregationService>();

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddGrpcClient<GrpcAccommodationSearch.AccommodationSearch.AccommodationSearchClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:14001");
});

builder.Services.AddGrpcClient<GrpcRatings.Ratings.RatingsClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:15001");
});
builder.Services.AddGrpcClient<GrpcIdentity.Identity.IdentityClient>((services, options) =>
{
    options.Address = new Uri("http://host.docker.internal:11001");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseMetricServer();
app.UseHttpMetrics();

// app.UseMiddleware<HttpRequestMetricsMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}");
});

app.MapControllers();

app.UseWebSockets();
await app.UseOcelot();

app.Run();
