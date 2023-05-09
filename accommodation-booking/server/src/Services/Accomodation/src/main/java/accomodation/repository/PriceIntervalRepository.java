package accomodation.repository;

import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import accomodation.model.PriceInterval;

@Repository
public interface PriceIntervalRepository extends JpaRepository<PriceInterval, UUID>{

}
