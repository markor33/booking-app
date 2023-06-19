using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class HostSearchAccommodationsDeleteUnsuccessfulIntegrationEvent : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        [JsonConstructor]
        public HostSearchAccommodationsDeleteUnsuccessfulIntegrationEvent(Guid hostId)
        {
            HostId = hostId;
        }
    }
}
