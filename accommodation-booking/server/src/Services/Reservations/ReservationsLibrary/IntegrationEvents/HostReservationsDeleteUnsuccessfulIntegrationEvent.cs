using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace ReservationsLibrary.IntegrationEvents
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
