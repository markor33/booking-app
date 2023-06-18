using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class HostAccommodationsDeleteFromRecommendationSystemUnsuccessfulIntegrationEvent : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        [JsonConstructor]
        public HostAccommodationsDeleteFromRecommendationSystemUnsuccessfulIntegrationEvent(Guid hostId)
        {
            HostId = hostId;
        }
    }
}
