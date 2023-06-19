import { Benefit } from "../../accomodation/models/benefit.model";
import { PriceRange } from "./price-range.model";

export class SearchQuery {
    location: string = '';
    numOfGuests: number = 0;
    start: Date = new Date();
    end: Date = new Date();
    priceRange: PriceRange = new PriceRange();
    benefits: Benefit[] = [];
    isHostProminent: boolean = false;
}