package accomodation.grpc.configurations;

import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import grpc_accommodation_search.AccommodationSearchGrpc;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class AccommodationSearchClientConfiguration {

    private String host = "host.docker.internal";
    private int port = 13001;

    @Bean
    public AccommodationSearchGrpc.AccommodationSearchBlockingStub accommodationSearchGrpcClient(){
        ManagedChannel channel = ManagedChannelBuilder.forAddress(host, port)
                .usePlaintext()
                .build();
        return AccommodationSearchGrpc.newBlockingStub(channel);
    }
}
