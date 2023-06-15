using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace Search.API.IntegrationEvents
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
