package accomodation.repository;

import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import accomodation.model.Benefit;

@Repository
public interface BenefitRepository extends JpaRepository<Benefit, UUID>{

}
