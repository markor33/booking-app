package accomodation.grpc;

import accomodation.dto.AccomodationDTO;
import accomodation.dto.PriceIntervalDTO;
import accomodation.enums.PriceType;
import accomodation.model.Accomodation;
import accomodation.model.Benefit;
import accomodation.model.PriceInterval;
import com.google.protobuf.Timestamp;
import grpc_accommodation_search.AccommodationSearchGrpc;
import grpc_accommodation_search.AccommodationSearchOuterClass;
import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import org.springframework.stereotype.Service;

import java.time.ZoneOffset;
import java.util.concurrent.ExecutionException;

@Service
public class AccommodationSearchGrpcService {
    private String host = "host.docker.internal";
    private int port = 13001;

    private final AccommodationSearchGrpc.AccommodationSearchBlockingStub accommodationSearchClient;

    public AccommodationSearchGrpcService() {
        ManagedChannel managedChannel = ManagedChannelBuilder.forAddress(host, port)
                .usePlaintext()
                .build();
        this.accommodationSearchClient = AccommodationSearchGrpc.newBlockingStub(managedChannel);
    }

    public boolean AddAccommodation(Accomodation accomodation) {
        AccommodationSearchOuterClass.AccommodationDTO.Builder accommodationGrpcDTO = AccommodationSearchOuterClass.AccommodationDTO.newBuilder()
                .setId(accomodation.getId().toString())
                .setHostId(accomodation.getHostId().toString())
                .setName(accomodation.getName())
                .setDescription(accomodation.getDescription())
                .setMinGuests(accomodation.getMinGuests())
                .setMaxGuests(accomodation.getMaxGuests())
                .setWeekendIncrease(accomodation.getWeekendIncrease())
                .setPhoto(accomodation.getPhotos().get(0).getUrl())
                .setGeneralPrice(accomodation.getGeneralPrice())
                .setLocation(AccommodationSearchOuterClass.Address.newBuilder()
                        .setCountry(accomodation.getLocation().getCountry())
                        .setCity(accomodation.getLocation().getCity())
                        .setStreet(accomodation.getLocation().getStreet())
                        .setNumber(accomodation.getLocation().getNumber())
                        .build())
                ;
        if (accommodationGrpcDTO.getPriceType().equals(PriceType.IN_WHOLE))
            accommodationGrpcDTO.setPriceType(AccommodationSearchOuterClass.PriceType.IN_WHOLE);
        else
            accommodationGrpcDTO.setPriceType(AccommodationSearchOuterClass.PriceType.PER_GUEST);
        for (Benefit benefit : accomodation.getBenefits())
            accommodationGrpcDTO.addBenefits(AccommodationSearchOuterClass.Benefit.newBuilder()
                            .setId(benefit.getId().toString())
                            .setName(benefit.getName())
                    .build());
        try {
            this.accommodationSearchClient.createAccommodation(accommodationGrpcDTO.build());
            return true;
        }
        catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }

    public boolean AddPriceInterval(PriceInterval priceInterval) {
        AccommodationSearchOuterClass.CreatePriceIntervalRequest createPriceIntervalRequest = AccommodationSearchOuterClass.CreatePriceIntervalRequest
                .newBuilder()
                .setId(priceInterval.getId().toString())
                .setAccommodationId(priceInterval.getAccomodation().getId().toString())
                .setAmount(priceInterval.getAmount())
                .setStartDate(Timestamp
                        .newBuilder()
                        .setSeconds(priceInterval.getInterval().getStart().toEpochSecond(ZoneOffset.UTC))
                        .setNanos(priceInterval.getInterval().getStart().getNano())
                        .build())
                .setEndDate(Timestamp
                        .newBuilder()
                        .setSeconds(priceInterval.getInterval().getEnd().toEpochSecond(ZoneOffset.UTC))
                        .setNanos(priceInterval.getInterval().getEnd().getNano())
                        .build())
                .build();
        try {
            this.accommodationSearchClient.createPriceInterval(createPriceIntervalRequest);
            return true;
        }
        catch (Exception ex) {
            ex.printStackTrace();
            return false;
        }
    }

    public boolean EditPriceInterval(PriceIntervalDTO priceInterval) {
        AccommodationSearchOuterClass.EditPriceIntervalRequest editPriceIntervalRequest = AccommodationSearchOuterClass.EditPriceIntervalRequest
                .newBuilder()
                .setAccommodationId(priceInterval.getAccommodationId().toString())
                .setPriceIntervalId(priceInterval.getId().toString())
                .setStartDate(Timestamp
                        .newBuilder()
                        .setSeconds(priceInterval.getInterval().getStart().toEpochSecond(ZoneOffset.UTC))
                        .setNanos(priceInterval.getInterval().getStart().getNano())
                        .build())
                .setEndDate(Timestamp
                        .newBuilder()
                        .setSeconds(priceInterval.getInterval().getEnd().toEpochSecond(ZoneOffset.UTC))
                        .setNanos(priceInterval.getInterval().getEnd().getNano())
                        .build())
                .setAmount(priceInterval.getAmount())
                .build();
        try {
            this.accommodationSearchClient.editPriceInterval(editPriceIntervalRequest);
            return true;
        }
        catch (Exception ex) {
            ex.printStackTrace();
            return false;
        }
    }

}
