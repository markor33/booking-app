package accomodation.repository;

import org.springframework.stereotype.Repository;

import accomodation.model.Accomodation;

import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;

@Repository
public interface AccomodationRepository extends JpaRepository<Accomodation, UUID>{

}
