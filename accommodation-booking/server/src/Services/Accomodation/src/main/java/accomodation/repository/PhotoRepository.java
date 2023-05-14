package accomodation.repository;

import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import accomodation.model.Photo;

@Repository
public interface PhotoRepository extends JpaRepository<Photo, UUID>{

}
