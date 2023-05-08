package accomodation.service;

import java.util.List;
import java.util.UUID;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import accomodation.dto.AccomodationDTO;
import accomodation.model.Accomodation;
import accomodation.repository.AccomodationRepository;

@Service
public class AccomodationService {

	@Autowired
	AccomodationRepository accomodationRepository;
	
	public List<Accomodation> findAll() {
		return accomodationRepository.findAll();
	}
	
	public Accomodation findById(UUID id) {
		return accomodationRepository.findById(id).orElseGet(null);
	}
	
	public Accomodation createAccomodation(AccomodationDTO accomodationDTO) {
		Accomodation accomodation = new Accomodation(accomodationDTO);
		return accomodationRepository.save(accomodation);
	}
	
}
