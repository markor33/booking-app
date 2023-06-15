using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace Identity.API.IntegrationEvents
{
    public class HostReservationsDeleteUnsuccessfulIntegrationEvent : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        [JsonConstructor]
        public HostReservationsDeleteUnsuccessfulIntegrationEvent(Guid hostId)
        {
            HostId = hostId;
        }

    }
}
