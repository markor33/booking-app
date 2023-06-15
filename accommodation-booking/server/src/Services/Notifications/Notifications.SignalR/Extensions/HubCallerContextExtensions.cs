using Microsoft.AspNetCore.SignalR;

namespace Notifications.SignalR.Extensions
{
    public static class HubCallerContextExtensions
    {
        public static Guid GetUserId(this HubCallerContext context)
        {
            var userId = context.GetHttpContext().Request.Query["userId"];
            return Guid.Parse(userId);
        }
    }
}
