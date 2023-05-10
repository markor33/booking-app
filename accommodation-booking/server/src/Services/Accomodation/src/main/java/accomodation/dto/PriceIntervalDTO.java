package accomodation.dto;

import accomodation.model.PriceInterval;
import accomodation.util.DateTimeRange;

import java.util.UUID;

public class PriceIntervalDTO {

    private UUID id;
    private UUID accommodationId;
    private DateTimeRange interval;
    private float amount;

    public PriceIntervalDTO() {
        super();
    }

    public PriceIntervalDTO(UUID id, UUID accommodationId, DateTimeRange interval, float amount) {
        this.id = id;
        this.accommodationId = accommodationId;
        this.interval = interval;
        this.amount = amount;
    }

    public PriceIntervalDTO(PriceInterval priceInterval) {
        this.id = priceInterval.getId();
        this.accommodationId = priceInterval.getAccomodation().getId();
        this.interval = priceInterval.getInterval();
        this.amount = priceInterval.getAmount();
    }

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public UUID getAccommodationId() {
        return accommodationId;
    }

    public void setAccommodationId(UUID accommodationId) {
        this.accommodationId = accommodationId;
    }

    public DateTimeRange getInterval() {
        return interval;
    }

    public void setInterval(DateTimeRange interval) {
        this.interval = interval;
    }

    public float getAmount() {
        return amount;
    }

    public void setAmount(float amount) {
        this.amount = amount;
    }
}
