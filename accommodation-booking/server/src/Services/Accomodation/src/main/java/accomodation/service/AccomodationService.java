package accomodation.service;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import accomodation.grpc.AccommodationSearchGrpcService;
import accomodation.grpc.ReservationsGrpcService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import accomodation.dto.AccomodationDTO;
import accomodation.model.Accomodation;
import accomodation.model.Address;
import accomodation.model.Photo;
import accomodation.repository.AccomodationRepository;
import accomodation.repository.AddressRepository;
import accomodation.repository.BenefitRepository;
import accomodation.repository.PhotoRepository;
import accomodation.repository.PriceIntervalRepository;
import accomodation.util.PhotoUploader;

@Service
public class AccomodationService {

	@Autowired
	AccommodationSearchGrpcService accommodationSearchGrpcService;

	@Autowired
	ReservationsGrpcService	 reservationsGrpcService;

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
	
	@Autowired
	PhotoUploader photoUploader;
	
	public List<Accomodation> findAll() {
		return accomodationRepository.findAll();
	}
	
	public List<Accomodation> findByHostId(UUID hostId) {
		return accomodationRepository.findByHostId(hostId);
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
			p.setUrl(photoUploader.uploadImage(p.getUrl()));
			newPhotos.add(p); 
		}
		createdAccomodation.setPhotos(photoRepository.saveAll(newPhotos));

		this.accommodationSearchGrpcService.AddAccommodation(createdAccomodation);
		this.reservationsGrpcService.AddAccommodation(createdAccomodation, accomodationDTO.isAutoConfirmation());

		return createdAccomodation;
	}
	
}
