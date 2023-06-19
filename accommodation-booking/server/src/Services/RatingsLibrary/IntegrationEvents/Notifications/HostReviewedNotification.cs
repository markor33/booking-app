using EventBus.NET.Integration;

namespace RatingsLibrary.IntegrationEvents.Notifications
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
