package accomodation.security.model;

import java.util.Objects;
import java.util.UUID;

import org.springframework.security.core.GrantedAuthority;

public class Role implements GrantedAuthority {

    private UUID id;
    private String name; //HOST ILI GUEST
    
    public Role() {
    	
    }
    
    public Role(UUID id, String name) {
		super();
		this.id = id;
		this.name = name;
	}

	public Role(Role r) {
		super();
		this.id = r.getId();
		this.name = r.getName();
	}

	public String getAuthority() {
        return name;
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
		Role other = (Role) obj;
		return Objects.equals(id, other.id) && Objects.equals(name, other.name);
	}

	@Override
	public String toString() {
		return "Role [id=" + id + ", name=" + name + "]";
	}
    
}