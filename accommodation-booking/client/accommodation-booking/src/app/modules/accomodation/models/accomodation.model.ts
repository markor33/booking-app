import { Address } from "../../auth/models/address.model";
import { Benefit } from "./benefit.model";
import { Photo } from "./photo.model";
import { PriceInterval } from "./price-interval.model";

export class Accomodation {
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
}

export enum PriceType {
    PER_GUEST = 'Per person',
    IN_WHOLE = 'In whole'
}