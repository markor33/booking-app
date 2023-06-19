import { Benefit } from "../../accomodation/models/benefit.model";
import { Photo } from "../../accomodation/models/photo.model";
import { PriceInterval } from "../../accomodation/models/price-interval.model";
import { Address } from "../../auth/models/address.model";
import { AccommodationRating } from "../../reservation/model/accommodation-rating.model";
import { HostRating } from "../../reservation/model/host-rating.model";

export class AccomodationDialog {
    id: string = '';
    hostId: string = '';
    name: string = '';
    description: string = '';
    minGuests: number = 0;
    maxGuests: number = 0;
    weekendIncrease: number = 0;
    created: Date = new Date();
    location: Address = new Address();
    benefits: Benefit[] = [];
    photos: Photo[] = [];
    priceIntervals: PriceInterval[] = [];
    generalPrice: number = 0;
    priceType: PriceType = PriceType.PER_GUEST;
    autoConfirmation: boolean = false;
    isHostProminent: boolean = false;
    avgHostGrade: number = 0;
    avgAccommGrade: number = 0;
    accommRatings: AccommodationRating[] = [];
    hostRatings: HostRating[] = []
}

export enum PriceType {
    PER_GUEST = 'Per person',
    IN_WHOLE = 'In whole'
}