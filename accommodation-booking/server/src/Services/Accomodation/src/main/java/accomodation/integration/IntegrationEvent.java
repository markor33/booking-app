package accomodation.integration;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.time.OffsetDateTime;
import java.util.UUID;

public class IntegrationEvent {
    protected UUID id;
    protected OffsetDateTime creationDate;

    public IntegrationEvent() {
        id = UUID.randomUUID();
        creationDate = OffsetDateTime.now();
    }

    public IntegrationEvent(UUID id, OffsetDateTime creationDate) {
        this.id = id;
        this.creationDate = creationDate;
    }

    @JsonProperty("Id")
    public UUID getId() {
        return id;
    }

    @JsonProperty("Id")
    public void setId(UUID id) {
        this.id = id;
    }

    @JsonProperty("CreationDate")
    public OffsetDateTime getCreationDate() {
        return creationDate;
    }

    @JsonProperty("CreationDate")
    public void setCreationDate(OffsetDateTime creationDate) {
        this.creationDate = creationDate;
    }
}
