import { AccommodationCard } from "../../accomodation/models/accommodation-card.model";
import { UserProfile } from "../../user/model/user-profile.model";
import { DateRange } from "./date-range.model";

export class Reservation {

    id: string;
    accommodationId: string;
    guestId: string;
    period: DateRange;
    numOfGuests: number;
    price: number;
    canceled: boolean;
    hRating: number = 0;
    aRating: number = 0;
    userFullName: string = "";
    accommPhoto: string = "";
    accommName: string = "";

    constructor(rrInter: ReservationRequestInterface){
        this.accommodationId = rrInter.accommodationId;
        this.guestId = rrInter.guestId;
        this.period = rrInter.period;
        this.numOfGuests = rrInter.numOfGuests;
        this.price = rrInter.price;
        this.canceled = rrInter.canceled
        this.id = rrInter.id;
    }
}

interface ReservationRequestInterface{
    id: string;
    accommodationId: string;
    guestId: string;
    period: DateRange;
    numOfGuests: number;
    price: number;
    canceled: boolean;
}