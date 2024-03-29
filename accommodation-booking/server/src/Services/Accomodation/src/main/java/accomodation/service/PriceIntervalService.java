package accomodation.service;

import java.util.List;
import java.util.UUID;

import accomodation.grpc.ReservationsGrpcService;
import accomodation.integration.eventBus.IEventBus;
import accomodation.integration.events.PriceIntervalChangedIntegrationEvent;
import accomodation.integration.events.PriceIntervalCreatedIntegrationEvent;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import accomodation.dto.PriceIntervalDTO;
import accomodation.grpc.AccommodationSearchGrpcService;
import accomodation.model.Accomodation;
import accomodation.model.PriceInterval;
import accomodation.repository.AccomodationRepository;
import accomodation.repository.PriceIntervalRepository;

@Service
public class PriceIntervalService {

    @Autowired
    IEventBus eventBus;

    @Autowired
    ReservationsGrpcService reservationsGrpcService;

    @Autowired
    PriceIntervalRepository priceIntervalRepository;

    @Autowired
    AccomodationRepository accomodationRepository;
      
    public List<PriceInterval> findByAccomodationId(UUID accomodationId) {
		return priceIntervalRepository.findByAccomodationId(accomodationId);
	}

    public PriceInterval create(PriceIntervalDTO priceIntervalDTO) {
    	List<PriceInterval> intervalsInAccomodation = findByAccomodationId(priceIntervalDTO.getAccommodationId());
    	for(PriceInterval pi : intervalsInAccomodation) {
    		if(pi.getInterval().overlapsWith(priceIntervalDTO.getInterval())) {
    			return null;
    		}
    	}
    	
        UUID id = UUID.randomUUID();
        Accomodation accomodation = accomodationRepository.findById(priceIntervalDTO.getAccommodationId()).get();

        boolean result = this.reservationsGrpcService.IsOverllaped(accomodation.getId(), priceIntervalDTO.getInterval());
        if(result)
            return null;

        PriceInterval priceInterval = new PriceInterval(id, accomodation, priceIntervalDTO.getAmount(), priceIntervalDTO.getInterval());
        priceInterval = this.priceIntervalRepository.save(priceInterval);

        this.eventBus.publish(
                new PriceIntervalCreatedIntegrationEvent(
                        priceInterval.getId(),
                        accomodation.getId(),
                        priceInterval.getAmount(),
                        priceInterval.getInterval().getStart(),
                        priceInterval.getInterval().getEnd()));

        return priceInterval;
    }
    
    public PriceInterval update(PriceIntervalDTO dto) {
    	List<PriceInterval> intervalsInAccomodation = findByAccomodationId(dto.getAccommodationId());
    	for(PriceInterval pi : intervalsInAccomodation) {
    		if(!dto.getId().equals(pi.getId()) && pi.getInterval().overlapsWith(dto.getInterval())) {
    			return null;
    		}
    	}  	
    	
    	Accomodation accomodation = accomodationRepository.findById(dto.getAccommodationId()).get();

        boolean result = this.reservationsGrpcService.IsOverllaped(accomodation.getId(), dto.getInterval());
    	if(result)
    		return null;

        // this.accommodationSearchGrpcService.EditPriceInterval(dto);

        eventBus.publish(new PriceIntervalChangedIntegrationEvent(
                dto.getId(),
                accomodation.getId(),
                dto.getAmount(),
                dto.getInterval().getStart(),
                dto.getInterval().getEnd())
        );

        return this.priceIntervalRepository.save(new PriceInterval(dto.getId(), accomodation, dto.getAmount(), dto.getInterval()));
    }

}
