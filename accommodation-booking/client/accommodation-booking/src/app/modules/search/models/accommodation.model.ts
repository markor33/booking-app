import { Address } from "../../shared/models/address.model";
import { Benefit } from "../../shared/models/benefit.model";
import { PriceType } from "../../shared/models/price-type.model";

export class Accommodation {
    id: string = '';
    hostId: string = '';
    name: string = '';
    description: string = '';
    location: Address = new Address();
    minGuests: number = 0;
    maxGuests: number = 0;
    photo: string = '';
    benefits: Benefit[] = [];
    priceType: PriceType = PriceType.PER_GUEST;
    price: number = 0;
    isHostProminent: boolean = false;
    avgHostGrade: number = 0;
    avgAccommGrade: number = 0;
}
