export class SearchQuery {
    location: string = '';
    numOfGuests: number = 0;
    start: Date = new Date();
    end: Date = new Date();
    filterArgs: Filters = new Filters();
}

export class Filters {
    minPrice: number | null = null;
    maxPrice: number | null = null;
    benefits: string[] = [];
}