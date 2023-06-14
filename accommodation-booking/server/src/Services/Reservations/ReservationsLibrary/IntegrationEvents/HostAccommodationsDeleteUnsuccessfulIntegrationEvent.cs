using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace ReservationsLibrary.IntegrationEvents
{
    public class HostAccommodationsDeleteUnsuccessfulIntegrationEvent : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        [JsonConstructor]
        public HostAccommodationsDeleteUnsuccessfulIntegrationEvent(Guid hostId)
        {
            HostId = hostId;
        }
    }
}
