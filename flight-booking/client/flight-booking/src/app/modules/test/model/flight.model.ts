export class Flight implements IFlight {
    id: string;
    from: string;
    to: string;

    constructor(flight: IFlight) {
        this.id = flight.id;
        this.from = flight.from;
        this.to = flight.to;
    }
}

interface IFlight {
    id: string,
    from: string,
    to: string
}