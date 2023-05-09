package accomodation.service;

import java.util.List;
import java.util.UUID;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import accomodation.dto.AccomodationDTO;
import accomodation.model.Accomodation;
import accomodation.model.Address;
import accomodation.repository.AccomodationRepository;
import accomodation.repository.AddressRepository;

@Service
public class AccomodationService {

	@Autowired
	AccomodationRepository accomodationRepository;
	
	@Autowired
	AddressRepository addressRepository;
	
	public List<Accomodation> findAll() {
		return accomodationRepository.findAll();
	}
	
	public Accomodation findById(UUID id) {
		return accomodationRepository.findById(id).orElseGet(null);
	}
	
	public Accomodation createAccomodation(AccomodationDTO accomodationDTO) {
		Address a = accomodationDTO.getLocation();
		a.setId(UUID.randomUUID());
		accomodationDTO.setLocation(addressRepository.save(a));
		Accomodation accomodation = new Accomodation(accomodationDTO);
		accomodation.setId(UUID.randomUUID());
		return accomodationRepository.save(accomodation);
	}
	
}
