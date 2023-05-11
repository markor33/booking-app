package accomodation.model;

import java.util.Date;
import java.util.List;
import java.util.Objects;
import java.util.UUID;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.JoinTable;
import javax.persistence.ManyToMany;
import javax.persistence.OneToMany;
import javax.persistence.OneToOne;
import javax.persistence.Table;

import org.hibernate.annotations.GenericGenerator;
import org.hibernate.annotations.Type;

import com.fasterxml.jackson.annotation.JsonIgnore;

import accomodation.dto.AccomodationDTO;
import accomodation.enums.PriceType;

@Entity
@Table(catalog = "db_accomodation", name = "accomodation") 
public class Accomodation {

	@Id
    @GeneratedValue(generator = "uuid2")
    @GenericGenerator(name = "uuid2", strategy = "uuid2")
    @Column(name = "id", columnDefinition = "VARCHAR(36)", updatable = false, nullable = false)
	@Type(type="uuid-char")
    private UUID id;
	
	@Column(name = "host_id", columnDefinition = "VARCHAR(36)")
	@Type(type="uuid-char")
	private UUID hostId;
	
	@Column(name = "name")
	private String name;
	
	@Column(name = "description")
	private String description;
	
	@Column(name = "min_guests")
	private int minGuests;
	
	@Column(name = "max_guests")
	private int maxGuests;
	
	@Column(name = "weekendIncrease")
	private int weekendIncrease;
	
	@Column(name = "created")
	private Date created = new Date();

	@OneToOne(cascade = CascadeType.REFRESH)
	@JoinColumn(name = "location_id", referencedColumnName = "id", columnDefinition = "VARCHAR(36)")
	private Address location;
	
	@JsonIgnore
	@ManyToMany
	@JoinTable(
			name = "accomodation_benefit",
			joinColumns = @JoinColumn(name = "accomodation_id"),
			inverseJoinColumns = @JoinColumn(name = "benefit_id")
	)
	private List<Benefit> benefits;
	
	@JsonIgnore
	@OneToMany(mappedBy = "accomodation", fetch = FetchType.LAZY)
	private List<Photo> photos;

	@Column(name = "general_price")
	private float generalPrice;
	
	@JsonIgnore
	@OneToMany(mappedBy = "accomodation", fetch = FetchType.LAZY)
	private List<PriceInterval> priceIntervals;
	
	@Column(name = "price_type")
	private PriceType priceType;
	
	public Accomodation() {

	}
	
	public Accomodation(UUID id, UUID hostId, String name, String description, int minGuests, int maxGuests, int weekendIncrease,
			Address location, List<Benefit> benefits, List<Photo> photos,
			List<PriceInterval> priceIntervals, PriceType priceType) {
		super();
		this.id = id;
		this.hostId = hostId;
		this.name = name;
		this.description = description;
		this.minGuests = minGuests;
		this.maxGuests = maxGuests;
		this.weekendIncrease = weekendIncrease;
		this.location = location;
		this.benefits = benefits;
		this.photos = photos;
		this.priceIntervals = priceIntervals;
		this.priceType = priceType;
	}

	public Accomodation(Accomodation a) {
		super();
		this.id = a.getId();
		this.hostId = a.getHostId();
		this.name = a.getName();
		this.description = a.getDescription();
		this.minGuests = a.getMinGuests();
		this.maxGuests = a.getMaxGuests();
		this.weekendIncrease = a.getWeekendIncrease();
		this.location = a.getLocation();
		this.benefits = a.getBenefits();
		this.photos = a.getPhotos();
		this.priceIntervals = a.getPriceIntervals();
		this.priceType = a.getPriceType();
	}
	
	public Accomodation(AccomodationDTO dto) {
		super();
		this.hostId = dto.getHostId();
		this.name = dto.getName();
		this.description = dto.getDescription();
		this.minGuests = dto.getMinGuests();
		this.maxGuests = dto.getMaxGuests();
		this.weekendIncrease = dto.getWeekendIncrease();
		this.location = dto.getLocation();
		this.benefits = dto.getBenefits();
		this.photos = dto.getPhotos();
		this.priceIntervals = dto.getPriceIntervals();
		this.generalPrice = dto.getGeneralPrice();
		this.priceType = dto.getPriceType();
	}

	public UUID getId() {
		return id;
	}

	public void setId(UUID id) {
		this.id = id;
	}

	public UUID getHostId() {
		return hostId;
	}

	public void setHostId(UUID hostId) {
		this.hostId = hostId;
	}
	
	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public int getMinGuests() {
		return minGuests;
	}

	public void setMinGuests(int minGuests) {
		this.minGuests = minGuests;
	}

	public int getMaxGuests() {
		return maxGuests;
	}

	public void setMaxGuests(int maxGuests) {
		this.maxGuests = maxGuests;
	}

	public int getWeekendIncrease() {
		return weekendIncrease;
	}

	public void setWeekendIncrease(int weekendIncrease) {
		this.weekendIncrease = weekendIncrease;
	}

	public Date getCreated() {
		return created;
	}

	public void setCreated(Date created) {
		this.created = created;
	}

	public Address getLocation() {
		return location;
	}

	public void setLocation(Address location) {
		this.location = location;
	}

	public List<Benefit> getBenefits() {
		return benefits;
	}

	public void setBenefits(List<Benefit> benefits) {
		this.benefits = benefits;
	}

	public List<Photo> getPhotos() {
		return photos;
	}

	public void setPhotos(List<Photo> photos) {
		this.photos = photos;
	}

	public List<PriceInterval> getPriceIntervals() {
		return priceIntervals;
	}

	public void setPriceIntervals(List<PriceInterval> priceIntervals) {
		this.priceIntervals = priceIntervals;
	}

	public PriceType getPriceType() {
		return priceType;
	}

	public void setPriceType(PriceType priceType) {
		this.priceType = priceType;
	}

	public float getGeneralPrice() {return generalPrice; }

	public void setGeneralPrice(float generalPrice) { this.generalPrice = generalPrice; }

	@Override
	public int hashCode() {
		return Objects.hash(benefits, created, description, generalPrice, hostId, id, location, maxGuests, minGuests,
				name, photos, priceIntervals, priceType, weekendIncrease);
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Accomodation other = (Accomodation) obj;
		return Objects.equals(benefits, other.benefits) && Objects.equals(created, other.created)
				&& Objects.equals(description, other.description)
				&& Float.floatToIntBits(generalPrice) == Float.floatToIntBits(other.generalPrice)
				&& Objects.equals(hostId, other.hostId) && Objects.equals(id, other.id)
				&& Objects.equals(location, other.location) && maxGuests == other.maxGuests
				&& minGuests == other.minGuests && Objects.equals(name, other.name)
				&& Objects.equals(photos, other.photos) && Objects.equals(priceIntervals, other.priceIntervals)
				&& priceType == other.priceType && weekendIncrease == other.weekendIncrease;
	}

	@Override
	public String toString() {
		return "Accomodation [id=" + id + ", hostId=" + hostId + ", name=" + name + ", description=" + description
				+ ", minGuests=" + minGuests + ", maxGuests=" + maxGuests + ", weekendIncrease=" + weekendIncrease
				+ ", created=" + created + ", location=" + location + ", benefits=" + benefits + ", photos=" + photos
				+ ", generalPrice=" + generalPrice + ", priceIntervals=" + priceIntervals + ", priceType=" + priceType
				+ "]";
	}
	
}