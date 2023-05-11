package accomodation.dto;

import java.util.Objects;

import accomodation.model.Accomodation;

public class AccomodationCardDTO {
	
	private String name;
	private String photoUrl;
	
	public AccomodationCardDTO() {
		super();
	}

	public AccomodationCardDTO(String name, String photoUrl) {
		super();
		this.name = name;
		this.photoUrl = photoUrl;
	}

	public AccomodationCardDTO(Accomodation a) {
		super();
		this.name = a.getName();
		this.photoUrl = a.getPhotos().get(0).getUrl();
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getPhotoUrl() {
		return photoUrl;
	}

	public void setPhotoUrl(String photoUrl) {
		this.photoUrl = photoUrl;
	}

	@Override
	public int hashCode() {
		return Objects.hash(name, photoUrl);
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		AccomodationCardDTO other = (AccomodationCardDTO) obj;
		return Objects.equals(name, other.name) && Objects.equals(photoUrl, other.photoUrl);
	}

	@Override
	public String toString() {
		return "AccomodationCardDTO [name=" + name + ", photoUrl=" + photoUrl + "]";
	}
	
}
