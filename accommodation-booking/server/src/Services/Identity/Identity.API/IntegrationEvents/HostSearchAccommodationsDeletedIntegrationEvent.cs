using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace Identity.API.IntegrationEvents
{
    public class HostSearchAccommodationsDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        [JsonConstructor]
        public HostSearchAccommodationsDeletedIntegrationEvent(Guid hostId)
        {
            HostId = hostId;
        }
    }
}
