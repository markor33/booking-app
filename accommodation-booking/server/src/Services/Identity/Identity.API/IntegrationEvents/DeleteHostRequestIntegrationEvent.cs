using EventBus.NET.Integration;

namespace Identity.API.IntegrationEvents
{
    public class DeleteHostRequestIntegrationEvent : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        public DeleteHostRequestIntegrationEvent(Guid hostId)
        {
            HostId = hostId;
        }
    }
}
