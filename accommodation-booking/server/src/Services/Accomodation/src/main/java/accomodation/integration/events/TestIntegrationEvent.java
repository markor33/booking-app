package accomodation.integration.events;

import accomodation.integration.IntegrationEvent;
import com.fasterxml.jackson.annotation.JsonProperty;

public class TestIntegrationEvent extends IntegrationEvent {
    private String name;

    public TestIntegrationEvent() {
        super();
    }

    public TestIntegrationEvent(String name) {
        super();
        this.name = name;
    }

    @JsonProperty("Name")
    public String getName() {
        return name;
    }

    @JsonProperty("Name")
    public void setName(String name) {
        this.name = name;
    }
}
