export class DateRange {

    start: Date;
    end: Date;
    
    constructor(interDateRange: DateRangeInterface){
        this.start = interDateRange.start;
        this.end = interDateRange.end;
    }
}

interface DateRangeInterface{
    start: Date;
    end: Date;
}