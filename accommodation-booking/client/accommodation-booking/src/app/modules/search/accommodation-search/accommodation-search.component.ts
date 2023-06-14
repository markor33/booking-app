import { Component } from '@angular/core';
import { AccommodationSearchService } from '../services/accommodation-search.service';
import { SearchQuery } from '../models/search-query.model';
import { Accommodation } from '../models/accommodation.model';
import { MatDialog } from '@angular/material/dialog';
import { AccommodationDisplayDialogComponent } from '../accommodation-display-dialog/accommodation-display-dialog.component';
import { DateRange } from '../../reservation/model/date-range.model';
import { Request } from '../models/request.model';
import { ReservationRequestService } from '../../reservation/service/reservation-request.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-accommodation-search',
  templateUrl: './accommodation-search.component.html',
  styleUrls: ['./accommodation-search.component.scss']
})
export class AccommodationSearchComponent {

  searchQuery: SearchQuery = new SearchQuery();
  numOfNights: number = 0;
  accommodations: Accommodation[] = [];
  request: Request;
  period: DateRange;
  isUserLogged: boolean = false;
  userRole: string = '';

  constructor(private searchService: AccommodationSearchService,
              private requestService: ReservationRequestService,
              private snackBar: MatSnackBar,
              private authService: AuthService,
              private matDialog: MatDialog) {
    this.period = {
      start: new Date(),
      end: new Date()
    }
    this.request = {
      accommodationId: "",
      period: this.period,
      numOfGuests: 0,
      price: 0,
    }
  }

  ngOnInit() {
    this.authService.loginObserver.subscribe((val) => {
      this.isUserLogged = val;
      if(this.isUserLogged)
        this.userRole = this.authService.getUserRole();
    });
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
    const diffTime =  Math.abs(this.searchQuery.end.getTime() - this.searchQuery.start.getTime())
    return Math.ceil(diffTime / (1000 * 60 * 60 * 24));
  }


  openAccommodationDisplay(id: string) {
    this.matDialog.open(AccommodationDisplayDialogComponent, {
      data: { id: id },
      width: '80%',
      height: '90%'
    });
  }

  createRequest(accommodationId: string, price: number){
    this.request.period.start = this.searchQuery.start;
    this.request.period.end = this.searchQuery.end;
    this.request.numOfGuests = this.searchQuery.numOfGuests;
    this.request.price = price;
    this.request.accommodationId = accommodationId;

    this.requestService.createRequest(this.request).subscribe({
      complete:() =>{
        this.snackBar.open("Reservation request is created!", "Ok", {
          duration: 2000,
          panelClass: ['blue-snackbar']
        });
      },
      error: (err) => {
        console.log(err);
        this.snackBar.open("Already is reserved", "Ok", {
          duration: 2000,
          panelClass: ['red-snackbar']
        });
      }
    })
  }

}
