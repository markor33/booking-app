import { Accomodation } from "./accomodation.model";
import { DateTimeRange } from "./date-time-range.model";

export class PriceInterval {
    id: string = '';
    accomodation: Accomodation = new Accomodation();
    amount: number = 0;
    interval: DateTimeRange = {
        start: new Date(),
        end: new Date()
    }
}