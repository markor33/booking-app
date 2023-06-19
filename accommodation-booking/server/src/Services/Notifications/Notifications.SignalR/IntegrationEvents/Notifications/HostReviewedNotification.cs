using EventBus.NET.Integration;

namespace Notifications.SignalR.IntegrationEvents.Notifications
{
    public class HostReviewedNotification : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        public HostReviewedNotification(Guid hostId)
        {
            HostId = hostId;
        }
    }
}
