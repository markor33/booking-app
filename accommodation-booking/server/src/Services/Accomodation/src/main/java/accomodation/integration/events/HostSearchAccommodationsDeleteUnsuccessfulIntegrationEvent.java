package accomodation.integration.events;

import accomodation.integration.IntegrationEvent;
import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.UUID;

public class HostSearchAccommodationsDeleteUnsuccessfulIntegrationEvent extends IntegrationEvent {
    private UUID hostId;

    public HostSearchAccommodationsDeleteUnsuccessfulIntegrationEvent() {
    }

    public HostSearchAccommodationsDeleteUnsuccessfulIntegrationEvent(UUID hostId) {
        this.hostId = hostId;
    }

    @JsonProperty("HostId")
    public UUID getHostId() {
        return hostId;
    }

    @JsonProperty("HostId")
    public void setHostId(UUID hostId) {
        this.hostId = hostId;
    }
}
