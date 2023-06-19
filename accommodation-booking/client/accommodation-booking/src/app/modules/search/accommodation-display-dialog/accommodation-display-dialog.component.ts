import { Component, Inject } from '@angular/core';
import { AccomodationService } from '../../accomodation/services/accomodation.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AccomodationDialog } from '../models/accommodationDialog.model';
import { Accomodation } from '../../accomodation/models/accomodation.model';
import { ReservationRequestService } from '../../reservation/service/reservation-request.service';
import { AccommodationSearchService } from '../services/accommodation-search.service';
import { Accommodation } from '../models/accommodation.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DateRange } from '../../reservation/model/date-range.model';
import { Request } from '../models/request.model';

@Component({
  selector: 'app-accommodation-display-dialog',
  templateUrl: './accommodation-display-dialog.component.html',
  styleUrls: ['./accommodation-display-dialog.component.scss']
})
export class AccommodationDisplayDialogComponent {

  accommodation: AccomodationDialog = new AccomodationDialog();
  stars: number[] = [1, 2, 3, 4, 5];
  availability!: { id: string, numOfGuests: number, start: Date, end: Date};
  availableAccommodation: Accommodation | null = null;
  numOfNights: number = 0;
  request: Request;
  period: DateRange;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private accommodationService: AccomodationService,
    private requestService: ReservationRequestService,
    private searchService: AccommodationSearchService,
    private snackBar: MatSnackBar) {
      this.accommodationService.getAccommodationDialog(data.accommId as string, data.hostId as string).subscribe((res) => {
        this.accommodation = res
      });
      this.availability = {
        id: data.id,
        numOfGuests: 0,
        start: new Date(),
        end: new Date()
      };
      this.period = {
        start: new Date(),
        end: new Date()
      };
      this.request = {
        accommodationId: data.id,
        period: this.period,
        numOfGuests: 0,
        price: 0,
      }
  }

  getBenefitNames(): string {
    return this.accommodation.benefits.map(benefit => benefit.name).join(', ');
  }

  check() {
    this.availableAccommodation = null;
    this.searchService.checkAvailability(this.availability).subscribe(
      (res) => {
        this.availableAccommodation = res;
        this.numOfNights = this.getNumOfNights();
      },
      (err) => this.snackBar.open('Not available', 'Ok', { duration: 2000 }));
  }

  getNumOfNights() {
    const diffTime =  Math.abs(this.availability.end.getTime() - this.availability.start.getTime())
    return Math.ceil(diffTime / (1000 * 60 * 60 * 24));
  }

  createRequest(accommodationId: string, price: number){
    this.request.period.start = this.availability.start;
    this.request.period.end = this.availability.end;
    this.request.numOfGuests = this.availability.numOfGuests;
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
