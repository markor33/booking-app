package accomodation.controller;

import java.util.List;
import java.util.ArrayList;
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

import accomodation.dto.AccomodationDTO;
import accomodation.model.Accomodation;
import accomodation.security.TokenUtils;
import accomodation.service.AccomodationService;

@RestController
@RequestMapping(value = "api/accomodation")
public class AccomodationController {
	
	@Autowired
	AccomodationService accomodationService;

	@Autowired
	GreeterService greeterService;
	
	@Autowired
	TokenUtils tokenUtils;

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
	
	@GetMapping(value = "/{id}")
	@PreAuthorize("hasAuthority('HOST')")
	public ResponseEntity<AccomodationDTO> getAccomodation(HttpServletRequest request, @PathVariable UUID id) {
		return new ResponseEntity<AccomodationDTO>(new AccomodationDTO(accomodationService.findById(id)), HttpStatus.OK);
	}
	
	@PostMapping()
	@PreAuthorize("hasAuthority('HOST')")
	public ResponseEntity<AccomodationDTO> createAccomodation(HttpServletRequest request, @RequestBody AccomodationDTO accomodationDTO) {
		//grpc posalji accomodationDTO.autoConfirmation reservation.api 
		UUID str = UUID.fromString(tokenUtils.getIdFromToken(tokenUtils.getToken(request)));
		accomodationDTO.setHostId(str);
		return new ResponseEntity<AccomodationDTO>(new AccomodationDTO(accomodationService.createAccomodation(accomodationDTO)), HttpStatus.OK);
	}
	
}
