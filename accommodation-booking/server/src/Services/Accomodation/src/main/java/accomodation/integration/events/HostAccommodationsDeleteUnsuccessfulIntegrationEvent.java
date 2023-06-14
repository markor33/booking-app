package accomodation.integration.events;

import accomodation.integration.IntegrationEvent;

import java.util.UUID;

public class HostAccommodationsDeleteUnsuccessfulIntegrationEvent extends IntegrationEvent {
    private UUID hostId;

    public HostAccommodationsDeleteUnsuccessfulIntegrationEvent(UUID hostId) {
        this.hostId = hostId;
    }

    public UUID getHostId() {
        return hostId;
    }

    public void setHostId(UUID hostId) {
        this.hostId = hostId;
    }
}
