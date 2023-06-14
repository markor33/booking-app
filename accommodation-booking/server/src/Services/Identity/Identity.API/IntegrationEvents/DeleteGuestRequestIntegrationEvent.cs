using EventBus.NET.Integration;

namespace Identity.API.IntegrationEvents
{
    public class DeleteGuestRequestIntegrationEvent : IntegrationEvent
    {
        public Guid GuestId { get; private set; }

        public DeleteGuestRequestIntegrationEvent(Guid guestId)
        {
            GuestId = guestId;
        }
    }
}
