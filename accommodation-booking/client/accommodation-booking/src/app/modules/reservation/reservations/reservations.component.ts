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

  constructor(private reservationService: ReservationService,
              private userService: ApplicationUserService,
              private snackBar: MatSnackBar,
              private authService: AuthService,
              private accommService: AccomodationService,
              private ratingsService: RatingsService){
    this.reservations = [];
    this.hostRatings = [];
    this.accommRatings = [];
    this.hostRating = {
      hostId: "",
      guestId: "",
      grade: 0,
      dateTimeOfGrade: new Date()
    }
    this.accommRating = {
      accommodationId: "",
      guestId: "",
      grade: 0,
      dateTimeOfGrade: new Date()
    }
  }

  ngOnInit(): void {
    this.authService.loginObserver.subscribe((val) => {
      this.isUserLogged = val;
      if(this.isUserLogged)
        this.userRole = this.authService.getUserRole();
    });
    this.getByUser();
  }

  getByUser(){
    this.reservationService.getRequestByUser().subscribe((res) =>{
      this.reservations = res;
      res.forEach((res) => {
        this.hostRatings.push(0)
        this.accommRatings.push(0)
      })
      this.include();
    });
  }

  rateHost(hostRating: number, i: number, resId: string, accommId: string): void {
    if(this.userRole == 'GUEST'){
      this.hostRating.guestId = accommId;
      this.hostRating.hostId = accommId;
      this.hostRatings[i] = hostRating;
      this.hostRating.grade = hostRating;
      this.ratingsService.rateHost(this.hostRating, resId).subscribe((res) =>{
        console.log("host rating created");
      })
    }
    
  }
  rateAccomm(accommRating: number, i: number, accommId: string): void{
    if(this.userRole == 'GUEST'){
      this.accommRatings[i] = accommRating;
      this.accommRating.grade = accommRating;
      this.accommRating.accommodationId = accommId;
      this.accommRating.guestId = accommId;
      this.ratingsService.rateAccommodation(this.accommRating).subscribe((res) =>{
        console.log("accommodation rating created");
      })
    }
  }
  deleteAccommRating(accommId: string, i: number){
    this.ratingsService.deleteAccommodationRate(accommId).subscribe(()=>{
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
}
