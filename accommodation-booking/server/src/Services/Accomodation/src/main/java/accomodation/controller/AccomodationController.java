package accomodation.controller;

import java.util.List;
import java.util.ArrayList;
import java.util.UUID;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import accomodation.dto.AccomodationDTO;
import accomodation.model.Accomodation;
import accomodation.service.AccomodationService;

@RestController
@RequestMapping(value = "api/accomodation")
public class AccomodationController {
	
	@Autowired
	AccomodationService accomodationService;

	@GetMapping(value = "/test")
	public ResponseEntity<String> getTest() {
		return new ResponseEntity<String>("asdasdasdasd", HttpStatus.OK);
	}
	
	@GetMapping()
	//@PreAuthorize("hasAuthority('host)")
	public ResponseEntity<List<AccomodationDTO>> getAllAccomodations(HttpServletRequest request){
		List<AccomodationDTO> accomodations = new ArrayList<>();
		for(Accomodation a : accomodationService.findAll()) {
			accomodations.add(new AccomodationDTO(a));
		}
		return new ResponseEntity<List<AccomodationDTO>>(accomodations, HttpStatus.OK);
	}
	
	@GetMapping(value = "/{id}")
	//@PreAuthorize("hasAuthority('host')")
	public ResponseEntity<AccomodationDTO> getAccomodation(HttpServletRequest request, @PathVariable String id) {
		return new ResponseEntity<AccomodationDTO>(new AccomodationDTO(accomodationService.findById(UUID.fromString(id))), HttpStatus.OK);
	}
	
	@PostMapping()
	//@PreAuthorize("hasAuthority('host')")
	public ResponseEntity<AccomodationDTO> createAccomodation(HttpServletRequest request, @RequestBody AccomodationDTO accomodationDTO) {
		return new ResponseEntity<AccomodationDTO>(new AccomodationDTO(accomodationService.createAccomodation(accomodationDTO)), HttpStatus.OK);
	}
	
}
