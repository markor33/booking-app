import { DateRange } from "./date-range.model";
import { Price } from "./price.model";
import { ReservationRequestStatus } from "./reservation-request-status";

export class ReservationRequest {
    
    accommodationId: string;
    guestId: string;
    period: DateRange;
    numOfGuests: number;
    price: Price;
    status: ReservationRequestStatus;

    constructor(rrInter: ReservationRequestInterface){
        this.accommodationId = rrInter.accommodationId;
        this.guestId = rrInter.guestId;
        this.period = rrInter.period;
        this.numOfGuests = rrInter.numOfGuests;
        this.price = rrInter.price;
        this.status = rrInter.status
    }
}

interface ReservationRequestInterface{
    accommodationId: string;
    guestId: string;
    period: DateRange;
    numOfGuests: number;
    price: Price;
    status: ReservationRequestStatus;
}

