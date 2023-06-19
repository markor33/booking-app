import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../service/reservation.service';
import { ApplicationUserService } from '../../user/service/application-user.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Reservation } from '../model/reservation.model';
import { AuthService } from '../../auth/services/auth.service';
import { PriceType } from '../model/price-type';
import { AccomodationService } from '../../accomodation/services/accomodation.service';
import { RatingsService } from '../service/ratings.service';
import { HostRating } from '../model/host-rating.model';
import { AccommodationRating } from '../model/accommodation-rating.model';
import { MatDialog } from '@angular/material/dialog';
import { FlightBookingDialogComponent } from '../flight-booking-dialog/flight-booking-dialog.component';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ReservationsComponent implements OnInit {

  reservations: Reservation[];
  isUserLogged: boolean = false;
  userRole: string = '';
  hostRatings: number[];
  accommRatings: number[];
  stars: number[] = [1, 2, 3, 4, 5];
  hostRating: HostRating ;
  accommRating: AccommodationRating;
  now: Date;

  constructor(private reservationService: ReservationService,
              private userService: ApplicationUserService,
              private snackBar: MatSnackBar,
              private authService: AuthService,
              private accommService: AccomodationService,
              private ratingsService: RatingsService,
              private dialog: MatDialog){
    this.reservations = [];
    this.hostRatings = [];
    this.accommRatings = [];
    this.hostRating = {
      hostId: "",
      guestId: "",
      grade: 0,
      dateTimeOfGrade: new Date(),
      reservationId: ""
    }
    this.accommRating = {
      accommodationId: "",
      guestId: "",
      grade: 0,
      dateTimeOfGrade: new Date(),
      reservationId: ""
    }
    this.now = new Date();
  }

  ngOnInit(): void {
    this.authService.loginObserver.subscribe((val) => {
      this.isUserLogged = val;
      if(this.isUserLogged)
        this.userRole = this.authService.getUserRole();
    });
    this.getByUser();
    this.now = new Date();
  }

  openFlightBooking(reservation: Reservation) {
    this.dialog.open(FlightBookingDialogComponent, {
      width: '50%',
      height: '50%',
      data: {
        accommodationId: reservation.accommodationId,
        period: reservation.period,
        numberOfPassengers: reservation.numOfGuests
      }
    });
  }

  getByUser(){
    this.reservationService.getRequestByUser().subscribe((res) =>{
      this.reservations = res;
      this.getHostRatings();
      this.getAcommRatings();
      this.include();
    });
  }

  rateHost(hostRating: number, i: number, resId: string, accommId: string): void {
    if(this.userRole == 'GUEST') {
      this.hostRating.guestId = accommId;
      this.hostRating.hostId = accommId;
      this.hostRating.reservationId = resId;
      this.hostRatings[i] = hostRating;
      this.hostRating.grade = hostRating;
      this.ratingsService.rateHost(this.hostRating, resId).subscribe((res) =>{
        console.log("host rating created");
      })
    }

  }
  getHostRatings(){
    this.ratingsService.getGradeHostByReservation().subscribe((res) =>{
      this.hostRatings = res;
    })
  }
  getAcommRatings(){
    this.ratingsService.getGradeAccommByReservation().subscribe((res) =>{
      this.accommRatings = res;
    })
  }
  rateAccomm(accommRating: number, i: number, accommId: string, resId: string): void{
    if(this.userRole == 'GUEST'){
      this.accommRatings[i] = accommRating;
      this.accommRating.grade = accommRating;
      this.accommRating.accommodationId = accommId;
      this.accommRating.reservationId = resId;
      this.accommRating.guestId = accommId;
      this.ratingsService.rateAccommodation(this.accommRating).subscribe((res) =>{
        console.log("accommodation rating created");
      })
    }
  }
  deleteAccommRating(resId: string, i: number){
    this.ratingsService.deleteAccommodationRate(resId).subscribe(()=>{
      console.log("accommodation rating deleted")
      this.accommRatings[i] = 0;
    })
  }

  deleteHostRating(resId: string, i: number){
    this.ratingsService.deleteHostRating(resId).subscribe(()=>{
      console.log("host rating deleted")
      this.hostRatings[i] = 0;
    })
  }

  include(){
    this.reservations.forEach((req) => {
      if(this.userRole == 'HOST')
      this.userService.getById(req.guestId).subscribe((res) => {
        req.guestProfile = res;
      })
      this.accommService.getAccommodationCoverAndName(req.accommodationId).subscribe((res) => {
        req.accommodationCard = res;
      })
    })
  }

  cancelReservation(id: string, i: number){
    this.reservationService.cancelReservation(id).subscribe({
      complete: () =>{
        this.snackBar.open("Reservation successfully canceled!", "Ok", {
          duration: 2000,
          panelClass: ['blue-snackbar']
        });
        this.reservations.splice(i, 1);
      },
      error: (err) => {
        console.log(err);
        this.snackBar.open("You cannot cancel a reservation 24 hours before", "Ok", {
          duration: 2000,
          panelClass: ['red-snackbar']
        });
      }
    });
  }

  printEnum(type: PriceType){
    if(type == 0)
      return "PER PERSON"
    return "IN WHOLE"
  }


compareDates(date1: Date, date2: Date): number{
  var d = new Date(date1);
  return d.getTime() - date2.getTime();
}

}
