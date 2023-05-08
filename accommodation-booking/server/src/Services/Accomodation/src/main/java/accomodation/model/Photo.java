package accomodation.model;

import java.util.Objects;
import java.util.UUID;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;

import org.hibernate.annotations.GenericGenerator;
import org.hibernate.annotations.Type;

import com.fasterxml.jackson.annotation.JsonIgnore;

@Entity
@Table(catalog = "db_accomodation", name = "photo") 
public class Photo {
	
	@Id
    @GeneratedValue(generator = "uuid2")
    @GenericGenerator(name = "uuid2", strategy = "uuid2")
    @Column(name = "id", columnDefinition = "VARCHAR(36)", updatable = false, nullable = false)
	@Type(type="uuid-char")
    private UUID id;
	
	@ManyToOne
	@JoinColumn(name = "accomodation_id")
	private Accomodation accomodation;
	
	@Column(name = "url")
	private String url;

	public Photo() {
		
	}

	public Photo(UUID id, Accomodation accomodation, String url) {
		super();
		this.id = id;
		this.accomodation = accomodation;
		this.url = url;
	}
	
	

	public Photo(Photo p) {
		super();
		this.id = p.getId();
		this.accomodation = p.getAccomodation();
		this.url = p.getUrl();
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

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}

	@Override
	public int hashCode() {
		return Objects.hash(accomodation, id, url);
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Photo other = (Photo) obj;
		return Objects.equals(accomodation, other.accomodation) && Objects.equals(id, other.id)
				&& Objects.equals(url, other.url);
	}

	@Override
	public String toString() {
		return "Photo [id=" + id + ", accomodation=" + accomodation + ", url=" + url + "]";
	}
	
}
