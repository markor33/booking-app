package accomodation.controller;

import accomodation.dto.PriceIntervalDTO;
import accomodation.service.PriceIntervalService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(value = "api/priceInterval")
public class PriceIntervalController {

    @Autowired
    PriceIntervalService priceIntervalService;

    @PostMapping()
    @PreAuthorize("hasAuthority('HOST')")
    public ResponseEntity<PriceIntervalDTO> create(@RequestBody PriceIntervalDTO priceIntervalDTO) {
        return new ResponseEntity<PriceIntervalDTO>(new PriceIntervalDTO(this.priceIntervalService.create(priceIntervalDTO)), HttpStatus.OK);
    }

}
