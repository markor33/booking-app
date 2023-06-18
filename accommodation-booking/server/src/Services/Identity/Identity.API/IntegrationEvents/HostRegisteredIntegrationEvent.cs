using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace Identity.API.IntegrationEvents
{
    public class HostRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        [JsonConstructor]
        public HostRegisteredIntegrationEvent(Guid hostId)
        {
            HostId = hostId;
        }

    }
}