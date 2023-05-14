import { AccommodationCard } from "../../accomodation/models/accommodation-card.model";
import { UserProfile } from "../../user/model/user-profile.model";
import { DateRange } from "./date-range.model";
import { ReservationRequestStatus } from "./reservation-request-status";

export class ReservationRequest {

    id: string;
    accommodationId: string;
    accommodationCard: AccommodationCard;
    guestProfile: UserProfile;
    guestId: string;
    period: DateRange;
    numOfGuests: number;
    price: number;
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
        this.accommodationCard = rrInter.accommodationCard;
    }
}

interface ReservationRequestInterface{
    id: string;
    accommodationId: string;
    guestProfile: UserProfile;
    guestId: string;
    period: DateRange;
    numOfGuests: number;
    price: number;
    status: ReservationRequestStatus;
    accommodationCard: AccommodationCard;
}

