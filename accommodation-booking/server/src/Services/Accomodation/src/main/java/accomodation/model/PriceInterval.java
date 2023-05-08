package accomodation.model;

import java.util.Objects;
import java.util.UUID;

import javax.persistence.AttributeOverride;
import javax.persistence.AttributeOverrides;
import javax.persistence.Column;
import javax.persistence.Embedded;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;

import org.hibernate.annotations.GenericGenerator;

import accomodation.util.DateTimeRange;

@Entity
@Table(catalog = "db_accomodation", name = "price_interval") 
public class PriceInterval {

	@Id
    @GeneratedValue(generator = "uuid2")
    @GenericGenerator(name = "uuid2", strategy = "org.hibernate.id.UUIDGenerator")
    @Column(name = "id", columnDefinition = "VARCHAR(255)", updatable = false, nullable = false)
    private UUID id;
	
	@ManyToOne
	@JoinColumn(name = "accomodation_id")
	private Accomodation accomodation;
	
	@Column(name = "amount")
	private float amount;
	
	@Embedded
	@AttributeOverrides({
		  @AttributeOverride( name = "start", column = @Column(name = "interval_start")),
		  @AttributeOverride( name = "end", column = @Column(name = "interval_end"))
		})
	private DateTimeRange interval;
	
	public PriceInterval() {
		
	}

	public PriceInterval(UUID id, Accomodation accomodation, float amount, DateTimeRange interval) {
		super();
		this.id = id;
		this.accomodation = accomodation;
		this.amount = amount;
		this.interval = interval;
	}

	public PriceInterval(PriceInterval p) {
		super();
		this.id = p.getId();
		this.accomodation = p.getAccomodation();
		this.amount = p.getAmount();
		this.interval = p.getInterval();
	}

	public UUID getId() {
		return id;
	}

	public void setId(UUID id) {
		this.id = id;
	}

	public Accomodation getAccomodation() {
		return accomodation;
	}

	public void setAccomodation(Accomodation accomodation) {
		this.accomodation = accomodation;
	}

	public float getAmount() {
		return amount;
	}

	public void setAmount(float amount) {
		this.amount = amount;
	}

	public DateTimeRange getInterval() {
		return interval;
	}

	public void setInterval(DateTimeRange interval) {
		this.interval = interval;
	}

	@Override
	public int hashCode() {
		return Objects.hash(accomodation, amount, id, interval);
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		PriceInterval other = (PriceInterval) obj;
		return Objects.equals(accomodation, other.accomodation)
				&& Float.floatToIntBits(amount) == Float.floatToIntBits(other.amount) && Objects.equals(id, other.id)
				&& Objects.equals(interval, other.interval);
	}

	@Override
	public String toString() {
		return "PriceInterval [id=" + id + ", accomodation=" + accomodation + ", amount=" + amount + ", interval="
				+ interval + "]";
	}
	
}
