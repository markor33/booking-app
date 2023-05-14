package accomodation.dto;

import accomodation.model.PriceInterval;
import accomodation.util.DateTimeRange;

import java.util.Objects;
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

	@Override
	public int hashCode() {
		return Objects.hash(accommodationId, amount, id, interval);
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		PriceIntervalDTO other = (PriceIntervalDTO) obj;
		return Objects.equals(accommodationId, other.accommodationId)
				&& Float.floatToIntBits(amount) == Float.floatToIntBits(other.amount) && Objects.equals(id, other.id)
				&& Objects.equals(interval, other.interval);
	}

	@Override
	public String toString() {
		return "PriceIntervalDTO [id=" + id + ", accommodationId=" + accommodationId + ", interval=" + interval
				+ ", amount=" + amount + "]";
	}
    
}
