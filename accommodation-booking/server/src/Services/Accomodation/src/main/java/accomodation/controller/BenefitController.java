package accomodation.controller;

import java.util.ArrayList;
import java.util.List;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import accomodation.dto.BenefitDTO;
import accomodation.model.Benefit;
import accomodation.service.BenefitService;

@RestController
@RequestMapping(value = "api/benefit")
public class BenefitController {

	@Autowired
	BenefitService benefitService;
	
	@GetMapping()
	@PreAuthorize("hasAuthority('HOST')")
	public ResponseEntity<List<BenefitDTO>> getAllBenefits(HttpServletRequest request){
		List<BenefitDTO> benefits = new ArrayList<>();
		for(Benefit b : benefitService.findAll()) {
			benefits.add(new BenefitDTO(b));
		}
		return new ResponseEntity<List<BenefitDTO>>(benefits, HttpStatus.OK);
	}
	
}
