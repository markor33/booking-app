package accomodation.model;

import java.util.List;
import java.util.Objects;
import java.util.UUID;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.ManyToMany;
import javax.persistence.Table;

import org.hibernate.annotations.GenericGenerator;

@Entity
@Table(catalog = "db_accomodation", name = "benefit") 
public class Benefit {

	@Id
    @GeneratedValue(generator = "uuid2")
    @GenericGenerator(name = "uuid2", strategy = "org.hibernate.id.UUIDGenerator")
    @Column(name = "id", columnDefinition = "VARCHAR(255)", updatable = false, nullable = false)
    private UUID id;
	
	@ManyToMany(mappedBy = "benefits")
	private List<Accomodation> accomodations;
	
	@Column(name = "name")
	private String name;
	
	public Benefit() {

	}
	
	public Benefit(UUID id, List<Accomodation> accomodations, String name) {
		super();
		this.id = id;
		this.accomodations = accomodations;
		this.name = name;
	}

	public Benefit(Benefit b) {
		super();
		this.id = b.getId();
		this.accomodations = b.getAccomodations();
		this.name = b.getName();
	}

	public UUID getId() {
		return id;
	}

	public void setId(UUID id) {
		this.id = id;
	}

	public List<Accomodation> getAccomodations() {
		return accomodations;
	}

	public void setAccomodations(List<Accomodation> accomodations) {
		this.accomodations = accomodations;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	@Override
	public int hashCode() {
		return Objects.hash(accomodations, id, name);
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Benefit other = (Benefit) obj;
		return Objects.equals(accomodations, other.accomodations) && Objects.equals(id, other.id)
				&& Objects.equals(name, other.name);
	}

	@Override
	public String toString() {
		return "Benefit [id=" + id + ", accomodations=" + accomodations + ", name=" + name + "]";
	}

}
