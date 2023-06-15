package accomodation.integration.events;

import accomodation.enums.PriceType;
import accomodation.integration.IntegrationEvent;
import accomodation.model.Accomodation;
import accomodation.model.Address;
import accomodation.model.Benefit;

import java.util.List;
import java.util.UUID;

public class AccommodationCreatedIntegrationEvent extends IntegrationEvent {
    private UUID accommodationId;
    private UUID hostId ;
    private String name ;
    private String description ;
    private Address location ;
    private int minGuests ;
    private int maxGuests ;
    private String photo ;
    private List<Benefit> benefits ;;
    private accomodation.enums.PriceType priceType;
    private float generalPrice;
    private int weekendIncrease;

    public AccommodationCreatedIntegrationEvent(Accomodation accomodation) {
        super();
        this.accommodationId = accomodation.getId();
        this.hostId = accomodation.getHostId();
        this.name = accomodation.getName();
        this.description = accomodation.getDescription();
        this.location = accomodation.getLocation();
        this.minGuests = accomodation.getMinGuests();
        this.maxGuests = accomodation.getMaxGuests();
        this.photo = accomodation.getPhotos().get(0).getUrl();
        this.benefits = accomodation.getBenefits();
        this.priceType = accomodation.getPriceType();
        this.generalPrice = accomodation.getGeneralPrice();
        this.weekendIncrease = accomodation.getWeekendIncrease();
    }

    public UUID getAccommodationId() {
        return accommodationId;
    }

    public void setAccommodationId(UUID accommodationId) {
        this.accommodationId = accommodationId;
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

    public Address getLocation() {
        return location;
    }

    public void setLocation(Address location) {
        this.location = location;
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

    public String getPhoto() {
        return photo;
    }

    public void setPhoto(String photo) {
        this.photo = photo;
    }

    public List<Benefit> getBenefits() {
        return benefits;
    }

    public void setBenefits(List<Benefit> benefits) {
        this.benefits = benefits;
    }

    public PriceType getPriceType() {
        return priceType;
    }

    public void setPriceType(PriceType priceType) {
        this.priceType = priceType;
    }

    public float getGeneralPrice() {
        return generalPrice;
    }

    public void setGeneralPrice(float generalPrice) {
        this.generalPrice = generalPrice;
    }

    public int getWeekendIncrease() {
        return weekendIncrease;
    }

    public void setWeekendIncrease(int weekendIncrease) {
        this.weekendIncrease = weekendIncrease;
    }
}
