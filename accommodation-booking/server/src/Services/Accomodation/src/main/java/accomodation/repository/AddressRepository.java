package accomodation.repository;

import org.springframework.stereotype.Repository;

import accomodation.model.Accomodation;
import accomodation.model.Address;

import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;

@Repository
public interface AddressRepository extends JpaRepository<Address, UUID>{

}
