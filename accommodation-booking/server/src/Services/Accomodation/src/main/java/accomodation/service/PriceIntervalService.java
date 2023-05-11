package accomodation.service;

import accomodation.dto.PriceIntervalDTO;
import accomodation.grpc.AccommodationSearchGrpcService;
import accomodation.model.Accomodation;
import accomodation.model.PriceInterval;
import accomodation.repository.AccomodationRepository;
import accomodation.repository.PriceIntervalRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class PriceIntervalService {

    @Autowired
    AccommodationSearchGrpcService accommodationSearchGrpcService;

    @Autowired
    PriceIntervalRepository priceIntervalRepository;

    @Autowired
    AccomodationRepository accomodationRepository;

    public PriceInterval create(PriceIntervalDTO priceIntervalDTO) {
        UUID id = UUID.randomUUID();

        Accomodation accomodation = accomodationRepository.findById(priceIntervalDTO.getAccommodationId()).get();

        PriceInterval priceInterval = new PriceInterval(id, accomodation, priceIntervalDTO.getAmount(), priceIntervalDTO.getInterval());

        priceInterval = this.priceIntervalRepository.save(priceInterval);

        this.accommodationSearchGrpcService.AddPriceInterval(priceInterval);

        return priceInterval;
    }

}
