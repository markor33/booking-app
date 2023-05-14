import { DateRange } from "../../reservation/model/date-range.model";

export class Request {
    
    accommodationId: string = '';
    period: DateRange;
    numOfGuests: number = 0;
    price: number;

    constructor(reqInter: RequestInterface){
        this.accommodationId = reqInter.accommodationId;
        this.period = reqInter.period;
        this.numOfGuests = reqInter.numOfGuests;
        this.price = reqInter.price;
    }
}

interface RequestInterface{
    accommodationId: string;
    period: DateRange;
    numOfGuests: number;
    price: number;
}
