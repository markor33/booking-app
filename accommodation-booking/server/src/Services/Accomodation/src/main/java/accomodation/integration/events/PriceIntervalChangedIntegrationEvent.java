package accomodation.integration.events;

import accomodation.integration.IntegrationEvent;

import java.time.LocalDateTime;
import java.util.UUID;

public class PriceIntervalChangedIntegrationEvent extends IntegrationEvent {
    private UUID priceIntervalId;
    private UUID accommodationId;
    private float amount;
    private LocalDateTime start;
    private LocalDateTime end;

    public PriceIntervalChangedIntegrationEvent(UUID priceIntervalId, UUID accommodationId, float amount, LocalDateTime start, LocalDateTime end) {
        this.priceIntervalId = priceIntervalId;
        this.accommodationId = accommodationId;
        this.amount = amount;
        this.start = start;
        this.end = end;
    }

    public UUID getPriceIntervalId() {
        return priceIntervalId;
    }

    public void setPriceIntervalId(UUID priceIntervalId) {
        this.priceIntervalId = priceIntervalId;
    }

    public UUID getAccommodationId() {
        return accommodationId;
    }

    public void setAccommodationId(UUID accommodationId) {
        this.accommodationId = accommodationId;
    }

    public float getAmount() {
        return amount;
    }

    public void setAmount(float amount) {
        this.amount = amount;
    }

    public LocalDateTime getStart() {
        return start;
    }

    public void setStart(LocalDateTime start) {
        this.start = start;
    }

    public LocalDateTime getEnd() {
        return end;
    }

    public void setEnd(LocalDateTime end) {
        this.end = end;
    }
}
