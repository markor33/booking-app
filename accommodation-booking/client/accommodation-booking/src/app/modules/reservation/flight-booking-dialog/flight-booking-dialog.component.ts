import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DateRange } from '../model/date-range.model';
import { AccomodationService } from '../../accomodation/services/accomodation.service';
import { FlightBookingService } from '../service/flight-booking.service';
import { Address } from '../../shared/models/address.model';
import { Flight } from '../model/flight.model';
import { AuthService } from '../../auth/services/auth.service';
import { BookedFlight } from '../model/booked-flight.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-flight-booking-dialog',
  templateUrl: './flight-booking-dialog.component.html',
  styleUrls: ['./flight-booking-dialog.component.scss']
})
export class FlightBookingDialogComponent {

  departureDestination: string = '';
  departureFlights: Flight[] = [];

  arrivalDestination: string = '';
  arrivalFlights: Flight[] = [];

  numberOfPassengers: number;

  accommodationId: string = '';
  accommodationLocation: Address = new Address();
  period: DateRange;

  hasAPIKey: boolean = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { accommodationId: string, period: DateRange, numberOfPassengers: number },
    private accommodationService: AccomodationService,
    private flightBookingService: FlightBookingService,
    private authService: AuthService,
    private snackBar: MatSnackBar
  ) {
    this.accommodationId = data.accommodationId;
    this.period = data.period;
    this.numberOfPassengers = data.numberOfPassengers;
    let apiKey = this.authService.getFlightBookingApiKey();
    if (apiKey !== null)
      this.hasAPIKey = true;
    else
    this.hasAPIKey = false;
  }

  ngOnInit() {
    this.accommodationService.getAccomodation(this.accommodationId).subscribe((accommodation) => {
      this.accommodationLocation = accommodation.location;
      console.log(this.accommodationLocation);
    });
  }

  search() {
    this.flightBookingService.search(this.period.start, this.departureDestination, this.accommodationLocation.city, this.numberOfPassengers)
    .subscribe((flights) => this.departureFlights = flights);

    this.flightBookingService.search(this.period.end, this.accommodationLocation.city, this.arrivalDestination, this.numberOfPassengers)
    .subscribe((flights) => this.arrivalFlights = flights);
  }

  book(flight: Flight) {
    var bookedFlight = new BookedFlight();
    bookedFlight.flightId = flight.id;
    bookedFlight.numberOfTickets = this.numberOfPassengers;
    this.flightBookingService.bookFlight(bookedFlight).subscribe((res) => {
      this.snackBar.open('Flight booked', 'Ok');
    });
  }

}
