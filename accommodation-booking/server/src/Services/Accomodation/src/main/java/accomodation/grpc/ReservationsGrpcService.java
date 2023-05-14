package accomodation.grpc;

import GrpcReservations.ReservationsGrpc;
import GrpcReservations.ReservationsOuterClass;
import accomodation.dto.AccomodationDTO;
import accomodation.model.Accomodation;
import accomodation.util.DateTimeRange;
import com.google.protobuf.Timestamp;
import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import org.springframework.stereotype.Service;

import java.time.ZoneOffset;
import java.util.UUID;

@Service
public class ReservationsGrpcService {

    private String host = "host.docker.internal";
    private int port = 12001;

    private ReservationsGrpc.ReservationsBlockingStub reservationsGrpcClient;

    public ReservationsGrpcService() {
        ManagedChannel channel = ManagedChannelBuilder.forAddress(host, port)
                .usePlaintext()
                .build();
        this.reservationsGrpcClient =  ReservationsGrpc.newBlockingStub(channel);
    }

    public boolean AddAccommodation(Accomodation accomodation, boolean autoConfirmation) {
        ReservationsOuterClass.AddAccommodationRequest addAccommodationRequest = ReservationsOuterClass.AddAccommodationRequest
                .newBuilder()
                .setAccommodationId(accomodation.getId().toString())
                .setHostId(accomodation.getHostId().toString())
                .setAutoConfirmation(autoConfirmation)
                .build();
        try {
            this.reservationsGrpcClient.addAccommodation(addAccommodationRequest);
            return true;
        }
        catch (Exception ex) {
            ex.printStackTrace();
            return false;
        }
    }

    public boolean IsOverllaped(UUID accommodationId, DateTimeRange range) {
        ReservationsOuterClass.IsOverLappedByAccomodationRequest isOverLappedByAccomodationRequest = ReservationsOuterClass.IsOverLappedByAccomodationRequest
                .newBuilder()
                .setAccommodationId(accommodationId.toString())
                .setStartDate(Timestamp.newBuilder()
                        .setSeconds(range.getStart().toEpochSecond(ZoneOffset.UTC))
                        .setNanos(range.getStart().getNano())
                        .build())
                .setEndDate(Timestamp.newBuilder()
                        .setSeconds(range.getEnd().toEpochSecond(ZoneOffset.UTC))
                        .setNanos(range.getEnd().getNano())
                        .build())
                .build();
        try {
            ReservationsOuterClass.IsOverLappedByAccomodationResponse response = this.reservationsGrpcClient.isOverLappedByAccomodation(isOverLappedByAccomodationRequest);
            return response.getIsOverlapped();
        }
        catch (Exception ex) {
            ex.printStackTrace();
            return false;
        }
    }

}
