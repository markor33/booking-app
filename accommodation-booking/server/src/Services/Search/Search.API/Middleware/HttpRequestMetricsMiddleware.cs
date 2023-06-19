using Prometheus;

namespace Search.API.Middleware
{
    public class HttpRequestMetricsMiddleware : IMiddleware
    {
        private static readonly Counter HttpRequestCountByIp = Metrics.CreateCounter(
        "http_requests_total_by_ip",
        "Number of HTTP requests by IP.",
        new CounterConfiguration
        {
            LabelNames = new[] { "ip", "user_agent", "timestamp" }
        });

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

            HttpRequestCountByIp.WithLabels(ip, userAgent, timestamp).Inc();

            await next(context);
        }
    }
}
