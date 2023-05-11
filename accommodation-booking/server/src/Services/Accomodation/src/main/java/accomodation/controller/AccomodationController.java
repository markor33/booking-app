package accomodation.controller;

import java.util.List;
import java.util.Map;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.UUID;

import javax.servlet.http.HttpServletRequest;

import accomodation.grpc.GreeterService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.cloudinary.Cloudinary;

import accomodation.dto.AccomodationCardDTO;
import accomodation.dto.AccomodationDTO;
import accomodation.model.Accomodation;
import accomodation.model.Photo;
import accomodation.security.TokenUtils;
import accomodation.service.AccomodationService;
import accomodation.util.PhotoUploader;

@RestController
@RequestMapping(value = "api/accomodation")
public class AccomodationController {
	
	@Autowired
	AccomodationService accomodationService;

	@Autowired
	GreeterService greeterService;
	
	@Autowired
	TokenUtils tokenUtils;

	@Autowired
	PhotoUploader photoUploader;
	
	@GetMapping(value = "/test")
	@PreAuthorize("hasAuthority('HOST')")
	public ResponseEntity<String> getTest(HttpServletRequest request) {
		String res = this.greeterService.receiveGreeting("ALOOOOOOOOOOOOOOOOOOOOOOOOOOO");
		System.out.println(res);
		return new ResponseEntity<String>("Test", HttpStatus.OK);
	}
	
	@GetMapping()
	@PreAuthorize("hasAuthority('HOST')")
	public ResponseEntity<List<AccomodationDTO>> getAllAccomodations(HttpServletRequest request){
		List<AccomodationDTO> accomodations = new ArrayList<>();
		for(Accomodation a : accomodationService.findAll()) {
			accomodations.add(new AccomodationDTO(a));
		}
		return new ResponseEntity<List<AccomodationDTO>>(accomodations, HttpStatus.OK);
	}
	
	@GetMapping(value = "/host/{hostId}")
	@PreAuthorize("hasAuthority('HOST')")
	public ResponseEntity<List<AccomodationDTO>> getAllAccomodationsForHost(HttpServletRequest request, @PathVariable UUID hostId){
		List<AccomodationDTO> accomodations = new ArrayList<>();
		for(Accomodation a : accomodationService.findByHostId(hostId)) {
			accomodations.add(new AccomodationDTO(a));
		}
		return new ResponseEntity<List<AccomodationDTO>>(accomodations, HttpStatus.OK);
	}
	
	@GetMapping(value = "/{id}")
	@PreAuthorize("hasAuthority('HOST')")
	public ResponseEntity<AccomodationDTO> getAccomodation(HttpServletRequest request, @PathVariable UUID id) {
		return new ResponseEntity<AccomodationDTO>(new AccomodationDTO(accomodationService.findById(id)), HttpStatus.OK);
	}
	
	@GetMapping(value = "/cover/{id}")
	@PreAuthorize("hasAuthority('HOST')")
	public ResponseEntity<AccomodationCardDTO> getAccomodationNameAndCoverPhoto(HttpServletRequest request, @PathVariable UUID id) {
		return new ResponseEntity<AccomodationCardDTO>(new AccomodationCardDTO(accomodationService.findById(id)), HttpStatus.OK);
	}
	
	@PostMapping()
	@PreAuthorize("hasAuthority('HOST')")
	public ResponseEntity<AccomodationDTO> createAccomodation(HttpServletRequest request, @RequestBody AccomodationDTO accomodationDTO) {
		//grpc posalji accomodationDTO.autoConfirmation reservation.api 
		UUID str = UUID.fromString(tokenUtils.getIdFromToken(tokenUtils.getToken(request)));
		accomodationDTO.setHostId(str);
		
		List<Photo> newPhotos = new ArrayList<Photo>();
		for(Photo p : accomodationDTO.getPhotos()) {
			p.setUrl(photoUploader.uploadImage(p.getUrl(), accomodationDTO.getName()));
			newPhotos.add(p);
		}
		
		return new ResponseEntity<AccomodationDTO>(new AccomodationDTO(accomodationService.createAccomodation(accomodationDTO)), HttpStatus.OK);
	}
	
}
