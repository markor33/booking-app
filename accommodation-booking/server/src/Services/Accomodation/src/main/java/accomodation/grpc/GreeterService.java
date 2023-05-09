package accomodation.grpc;

import io.grpc.ManagedChannel;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import search.GreeterGrpc;
import search.Search;

@Service
public class GreeterService {

    private final GreeterGrpc.GreeterBlockingStub greeterBlockingStub;

    @Autowired
    public GreeterService(ManagedChannel managedChannel) {
        this.greeterBlockingStub = GreeterGrpc.newBlockingStub(managedChannel);
    }

    public String receiveGreeting(String name) {
        Search.HelloRequest request = Search.HelloRequest.newBuilder()
                .setName(name)
                .build();
        return this.greeterBlockingStub.sayHello(request).getMessage();
    }

}
