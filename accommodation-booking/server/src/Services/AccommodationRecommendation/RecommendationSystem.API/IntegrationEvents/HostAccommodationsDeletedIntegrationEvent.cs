using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class HostAccommodationsDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        [JsonConstructor]
        public HostAccommodationsDeletedIntegrationEvent(Guid hostId)
        {
            HostId = hostId;
        }
    }
}
