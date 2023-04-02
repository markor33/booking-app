export class Flight {
    departureTime: Date;
    landingTime: Date;
    origin: string;
    destination: string;
    ticketPrice: number;
    numOfAvailableTickets: number;
    imgUrl: string;
    id: string;

    constructor(flightInter: FlightInterface){
        this.departureTime = flightInter.departureTime,
        this.landingTime = flightInter.landingTime,
        this.origin = flightInter.origin,
        this.destination = flightInter.destination,
        this.ticketPrice = flightInter.ticketPrice,
        this.numOfAvailableTickets = flightInter.numOfAvailableTickets,
        this.imgUrl = flightInter.imgUrl
        this.id = flightInter.id;
    }
}

interface FlightInterface {
    departureTime: Date;
    landingTime: Date;
    origin: string;
    destination: string;
    ticketPrice: number;
    numOfAvailableTickets: number;
    imgUrl: string;
    id: string;
}
