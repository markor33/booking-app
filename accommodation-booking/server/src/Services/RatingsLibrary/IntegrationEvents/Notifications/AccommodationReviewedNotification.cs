using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace RatingsLibrary.IntegrationEvents.Notifications
{
    public class AccommodationReviewedNotification : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        [JsonConstructor]
        public AccommodationReviewedNotification(Guid hostId)
        {
            HostId = hostId;
        }
    }
}
