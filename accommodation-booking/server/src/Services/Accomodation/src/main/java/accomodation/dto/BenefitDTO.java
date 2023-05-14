package accomodation.dto;

import java.util.Objects;
import java.util.UUID;

import accomodation.model.Benefit;

public class BenefitDTO {
	private UUID id;
	private String name;	

	public BenefitDTO() {
		super();
	}
	
	public BenefitDTO(Benefit b) {
		this.id = b.getId();
		this.name = b.getName();
	}
	
	public BenefitDTO(UUID id, String name) {
		super();
		this.id = id;
		this.name = name;
	}

	public UUID getId() {
		return id;
	}
	public void setId(UUID id) {
		this.id = id;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}

	@Override
	public int hashCode() {
		return Objects.hash(id, name);
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		BenefitDTO other = (BenefitDTO) obj;
		return Objects.equals(id, other.id) && Objects.equals(name, other.name);
	}

	@Override
	public String toString() {
		return "BenefitDTO [id=" + id + ", name=" + name + "]";
	}

}
