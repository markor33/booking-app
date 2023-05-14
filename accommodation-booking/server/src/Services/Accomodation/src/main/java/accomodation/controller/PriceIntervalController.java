package accomodation.controller;

import accomodation.dto.AccomodationDTO;
import accomodation.dto.PriceIntervalDTO;
import accomodation.model.Accomodation;
import accomodation.model.PriceInterval;
import accomodation.service.PriceIntervalService;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(value = "api/priceInterval")
public class PriceIntervalController {

    @Autowired
    PriceIntervalService priceIntervalService;
    
    @GetMapping(value = "/accomodation/{accomodationId}")
    @PreAuthorize("hasAuthority('HOST')")
    public ResponseEntity<List<PriceIntervalDTO>> getPriceIntervalsForAccomodation(HttpServletRequest request, @PathVariable UUID accomodationId) {
    	List<PriceIntervalDTO> priceIntervals = new ArrayList<>();
    	for(PriceInterval p : priceIntervalService.findByAccomodationId(accomodationId)) {
    		priceIntervals.add(new PriceIntervalDTO(p));
    	}
    	return new ResponseEntity<List<PriceIntervalDTO>>(priceIntervals, HttpStatus.OK);
    }
    
    @PostMapping()
    @PreAuthorize("hasAuthority('HOST')")
    public ResponseEntity<PriceIntervalDTO> createPriceInterval(HttpServletRequest request, @RequestBody PriceIntervalDTO priceIntervalDTO) {
       PriceInterval createdPriceInterval = this.priceIntervalService.create(priceIntervalDTO);
       
       if(createdPriceInterval != null) {
    	   return new ResponseEntity<PriceIntervalDTO>(new PriceIntervalDTO(createdPriceInterval), HttpStatus.OK);
       } else {
    	   return new ResponseEntity<PriceIntervalDTO>(HttpStatus.BAD_REQUEST);
       }
    	
    }
    
    @PutMapping(value = "/{id}")
    @PreAuthorize("hasAuthority('HOST')")
    public ResponseEntity<PriceIntervalDTO> updatePriceInterval(HttpServletRequest request, @PathVariable UUID id, @RequestBody PriceIntervalDTO priceIntervalDTO){
    	PriceInterval updatedPriceInterval = this.priceIntervalService.update(priceIntervalDTO);
    	
    	if(updatedPriceInterval != null) {
    		return new ResponseEntity<PriceIntervalDTO>(new PriceIntervalDTO(updatedPriceInterval), HttpStatus.OK);
    	} else {
    		return new ResponseEntity<PriceIntervalDTO>(HttpStatus.BAD_REQUEST);
    	}
    	
    }

}
