import { UserProfile } from "../../user/model/user-profile.model";
import { DateRange } from "./date-range.model";
import { Price } from "./price.model";
import { ReservationRequestStatus } from "./reservation-request-status";

export class ReservationRequest {

    id: string;
    accommodationId: string;
    guestProfile: UserProfile;
    guestId: string;
    period: DateRange;
    numOfGuests: number;
    price: Price;
    status: ReservationRequestStatus;

    constructor(rrInter: ReservationRequestInterface){
        this.accommodationId = rrInter.accommodationId;
        this.guestProfile = rrInter.guestProfile;
        this.guestId = rrInter.guestId;
        this.period = rrInter.period;
        this.numOfGuests = rrInter.numOfGuests;
        this.price = rrInter.price;
        this.status = rrInter.status
        this.id = rrInter.id;
    }
}

interface ReservationRequestInterface{
    id: string;
    accommodationId: string;
    guestProfile: UserProfile;
    guestId: string;
    period: DateRange;
    numOfGuests: number;
    price: Price;
    status: ReservationRequestStatus;
}

