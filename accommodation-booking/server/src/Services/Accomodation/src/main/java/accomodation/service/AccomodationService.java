package accomodation.service;

import java.util.List;
import java.util.ArrayList;
import java.util.UUID;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import accomodation.dto.AccomodationDTO;
import accomodation.model.Accomodation;
import accomodation.model.Address;
import accomodation.model.Benefit;
import accomodation.model.Photo;
import accomodation.model.PriceInterval;
import accomodation.repository.AccomodationRepository;
import accomodation.repository.AddressRepository;
import accomodation.repository.BenefitRepository;
import accomodation.repository.PhotoRepository;
import accomodation.repository.PriceIntervalRepository;

@Service
public class AccomodationService {

	@Autowired
	AccomodationRepository accomodationRepository;
	
	@Autowired
	AddressRepository addressRepository;
	
	@Autowired
	BenefitRepository benefitRepository;
	
	@Autowired
	PhotoRepository photoRepository;
	
	@Autowired
	PriceIntervalRepository priceIntervalRepository;
	
	public List<Accomodation> findAll() {
		return accomodationRepository.findAll();
	}
	
	public Accomodation findById(UUID id) {
		return accomodationRepository.findById(id).orElseGet(null);
	}
	
	public Accomodation createAccomodation(AccomodationDTO accomodationDTO) {
		UUID accomodationUUID = UUID.randomUUID();
		
		Address a = accomodationDTO.getLocation();
		a.setId(UUID.randomUUID());
		accomodationDTO.setLocation(addressRepository.save(a));
		
		Accomodation accomodation = new Accomodation(accomodationDTO);
		accomodation.setId(accomodationUUID);
				
		Accomodation createdAccomodation = accomodationRepository.save(accomodation);
		
		List<Photo> newPhotos = new ArrayList<Photo>(); 
		for(Photo p : accomodationDTO.getPhotos()) { 
			p.setId(UUID.randomUUID());
			p.setAccomodation(createdAccomodation);
			newPhotos.add(p); 
		}
		createdAccomodation.setPhotos(photoRepository.saveAll(newPhotos));
				
		return createdAccomodation;
	}
	
}
