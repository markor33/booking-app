using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Notifications.SignalR.Extensions;
using Notifications.SignalR.Services;

namespace Notifications.SignalR.Hubs
{
    public class NotificationsHub : Hub
    {
        private readonly IActiveUserService _activeUserService;

        public NotificationsHub(IActiveUserService activeUserService)
        {
            _activeUserService = activeUserService;
        }

        public override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            _activeUserService.UserConnected(Context.ConnectionId, Context.GetUserId());

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _activeUserService.UserDisconnected(Context.GetUserId());

            return base.OnDisconnectedAsync(exception);
        }
    }

}
