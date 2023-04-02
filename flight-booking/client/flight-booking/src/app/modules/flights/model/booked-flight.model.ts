import { Flight } from "./flight.model";

export class BookedFlight {

    flightId: string;
    flight: Flight;
    numberOfTickets: number;

    constructor(flightInter: BookedFlightInterface){
        this.flightId = flightInter.flightId;
        this.flight = flightInter.flight;
        this.numberOfTickets = flightInter.numberOfTickets;
    }
}

interface BookedFlightInterface {
    flightId: string;
    flight: Flight;
    numberOfTickets: number;
}
