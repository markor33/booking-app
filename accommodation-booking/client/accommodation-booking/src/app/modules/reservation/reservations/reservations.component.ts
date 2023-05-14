import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../service/reservation.service';
import { ApplicationUserService } from '../../user/service/application-user.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Reservation } from '../model/reservation.model';
import { AuthService } from '../../auth/services/auth.service';
import { PriceType } from '../model/price-type';
import { AccomodationService } from '../../accomodation/services/accomodation.service';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ReservationsComponent implements OnInit {

  reservations: Reservation[];
  isUserLogged: boolean = false;
  userRole: string = '';

  constructor(private reservationService: ReservationService,
              private userService: ApplicationUserService,
              private snackBar: MatSnackBar,
              private authService: AuthService,
              private accommService: AccomodationService){
    this.reservations = [];
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
      this.include();
    });
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
