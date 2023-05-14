import { Component } from '@angular/core';
import { AccommodationSearchService } from '../services/accommodation-search.service';
import { SearchQuery } from '../models/search-query.model';
import { Accommodation } from '../models/accommodation.model';
import { MatDialog } from '@angular/material/dialog';
import { AccommodationDisplayDialogComponent } from '../accommodation-display-dialog/accommodation-display-dialog.component';

@Component({
  selector: 'app-accommodation-search',
  templateUrl: './accommodation-search.component.html',
  styleUrls: ['./accommodation-search.component.scss']
})
export class AccommodationSearchComponent {

  searchQuery: SearchQuery = new SearchQuery();
  numOfNights: number = 0;
  accommodations: Accommodation[] = [];

  constructor(
    private searchService: AccommodationSearchService,
    private matDialog: MatDialog) {}

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

  openAccommodationDisplay(id: string) {
    this.matDialog.open(AccommodationDisplayDialogComponent, {
      data: { id: id },
      width: '80%',
      height: '90%'
    });
  }

}
