import { Component } from '@angular/core';
import { AccommodationSearchService } from '../services/accommodation-search.service';
import { SearchQuery } from '../models/search-query.model';
import { Accommodation } from '../models/accommodation.model';

@Component({
  selector: 'app-accommodation-search',
  templateUrl: './accommodation-search.component.html',
  styleUrls: ['./accommodation-search.component.scss']
})
export class AccommodationSearchComponent {

  searchQuery: SearchQuery = new SearchQuery();
  numOfNights: number = 0;
  accommodations: Accommodation[] = [];

  constructor(private searchService: AccommodationSearchService) {}

  ngOnInit() {

  }

  search() {
    this.searchService.search(this.searchQuery).subscribe((res) => 
    {
      this.accommodations = res;
      this.numOfNights = this.getNumOfNights();
      console.log(this.numOfNights);
    });
  }

  getNumOfNights() {
    const diffTime =  Math.abs(this.searchQuery.endDate.getTime() - this.searchQuery.startDate.getTime())
    return Math.ceil(diffTime / (1000 * 60 * 60 * 24));
  }

}
