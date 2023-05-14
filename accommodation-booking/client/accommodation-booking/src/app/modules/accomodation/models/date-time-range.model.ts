export class DateTimeRange implements DateTimeRangeInterface{

    public start: any;
    public end: any;

    constructor(dtri: DateTimeRangeInterface){
        this.start = dtri.start;
        this.end = dtri.end;
    }
}

interface DateTimeRangeInterface{
    start: any;
    end: any;
}