package accomodation.dto;

import java.util.Date;
import java.util.List;
import java.util.Objects;
import java.util.UUID;

import accomodation.enums.PriceType;
import accomodation.model.Accomodation;
import accomodation.model.Address;
import accomodation.model.Benefit;
import accomodation.model.Photo;
import accomodation.model.PriceInterval;

public class AccomodationDTO {
	
	private UUID id;
	private UUID hostId;
	private String name;
	private String description;
	private int minGuests;
	private int maxGuests;
	private int weekendIncrease;
	private Date created = new Date();
	private Address location;
	private List<Benefit> benefits;
	private List<Photo> photos;
	private List<PriceInterval> priceIntervals;
	private PriceType priceType;
	private boolean autoConfirmation;
	
	public AccomodationDTO() {
		super();
	}

	public AccomodationDTO(UUID id, UUID hostId, String name, String description, int minGuests, int maxGuests, int weekendIncrease,
			Date created, Address location, List<Benefit> benefits, List<Photo> photos,
			List<PriceInterval> priceIntervals, PriceType priceType, boolean autoConfirmation) {
		super();
		this.id = id;
		this.hostId = hostId;
		this.name = name;
		this.description = description;
		this.minGuests = minGuests;
		this.maxGuests = maxGuests;
		this.weekendIncrease = weekendIncrease;
		this.created = created;
		this.location = location;
		this.benefits = benefits;
		this.photos = photos;
		this.priceIntervals = priceIntervals;
		this.priceType = priceType;
		this.autoConfirmation = autoConfirmation;
	}

	public AccomodationDTO(Accomodation a) {
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
		this.autoConfirmation = false;
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

	public boolean isAutoConfirmation() {
		return autoConfirmation;
	}

	public void setAutoConfirmation(boolean autoConfirmation) {
		this.autoConfirmation = autoConfirmation;
	}

	@Override
	public int hashCode() {
		return Objects.hash(autoConfirmation, benefits, created, description, hostId, id, name, location, maxGuests,
				minGuests, photos, priceIntervals, priceType, weekendIncrease);
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		AccomodationDTO other = (AccomodationDTO) obj;
		return autoConfirmation == other.autoConfirmation && Objects.equals(benefits, other.benefits)
				&& Objects.equals(name, other.name)
				&& Objects.equals(created, other.created) && Objects.equals(description, other.description)
				&& Objects.equals(hostId, other.hostId) && Objects.equals(id, other.id)
				&& Objects.equals(location, other.location) && maxGuests == other.maxGuests
				&& minGuests == other.minGuests && Objects.equals(photos, other.photos)
				&& Objects.equals(priceIntervals, other.priceIntervals) && priceType == other.priceType
				&& weekendIncrease == other.weekendIncrease;
	}

	@Override
	public String toString() {
		return "AccomodationDTO [id=" + id + ", hostId=" + hostId + ", description=" + description + ", name=" + name + ", minGuests="
				+ minGuests + ", maxGuests=" + maxGuests + ", weekendIncrease=" + weekendIncrease + ", created="
				+ created + ", location=" + location + ", benefits=" + benefits + ", photos=" + photos
				+ ", priceIntervals=" + priceIntervals + ", priceType=" + priceType + ", autoConfirmation="
				+ autoConfirmation + "]";
	}

}
