package accomodation.repository;

import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import accomodation.model.Address;

@Repository
public interface AddressRepository extends JpaRepository<Address, UUID>{

}
