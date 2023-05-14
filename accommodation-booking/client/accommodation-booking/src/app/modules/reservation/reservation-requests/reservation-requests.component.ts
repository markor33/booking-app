import { Component, OnInit } from '@angular/core';
import { ReservationRequestService } from '../service/reservation-request.service';
import { ReservationRequest } from '../model/reservation-request.model';
import { ApplicationUserService } from '../../user/service/application-user.service';
import { PriceType } from '../model/price-type';
import { ReservationService } from '../service/reservation.service';
import { AuthService } from '../../auth/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AccomodationService } from '../../accomodation/services/accomodation.service';

@Component({
  selector: 'app-reservation-requests',
  templateUrl: './reservation-requests.component.html',
  styleUrls: ['./reservation-requests.component.scss']
})
export class ReservationRequestsComponent implements OnInit {

   requests: ReservationRequest[];
   cancelation: number[];
   isUserLogged: boolean = false;
   userRole: string = '';

  constructor(private reservationRequestService: ReservationRequestService,
     private userService: ApplicationUserService,
     private reservationService: ReservationService,
     private authService: AuthService,
     private snackBar: MatSnackBar,
     private accommService: AccomodationService){ 
    this.requests = [];
    this.cancelation = [];
  }
  ngOnInit(): void {
    this.authService.loginObserver.subscribe((val) => {
      this.isUserLogged = val;
      if(this.isUserLogged)
        this.userRole = this.authService.getUserRole();
    });
    this.getByUser();
  }
  approveRequest(id: string, i: number ){
    this.reservationRequestService.approveRequest(id).subscribe({
      complete: () =>{
        this.snackBar.open("Request successfully approved!", "Ok", {
          duration: 2000,
          panelClass: ['blue-snackbar']
        });
        this.requests.splice(i, 1);
      }
    });
    
  }
  declineRequest(id: string, i: number){
    this.reservationRequestService.declineRequest(id).subscribe({
      complete: () =>{
        this.snackBar.open("Request successfully declined!", "Ok", {
          duration: 2000,
          panelClass: ['blue-snackbar']
        });
        this.requests.splice(i, 1);
      }
    });
  }
  getByUser(){
    this.reservationRequestService.getRequestByUser().subscribe((res) =>{
      this.requests = res;
      this.include();
    });
  }
  deleteRequest(id: string, i: number){
    this.reservationRequestService.deleteRequest(id).subscribe({
      complete: () =>{
        this.snackBar.open("Request successfully deleted!", "Ok", {
          duration: 2000,
          panelClass: ['blue-snackbar']
        });
        this.requests.splice(i, 1);
      }
    });
  }
  getNumberOfCancelationForGuest(id: string){
     this.reservationService.getNumberOfCancelationforGuest(id).subscribe((res) =>{
      this.cancelation.push(res);
     })
  }
  include(){
    this.requests.forEach((req) => {
      if(this.userRole == 'HOST'){
        this.getNumberOfCancelationForGuest(req.guestId);
        this.userService.getById(req.guestId).subscribe((res) => {
          req.guestProfile = res;
        })
      }
      this.accommService.getAccommodationCoverAndName(req.accommodationId).subscribe((res) => {
        req.accommodationCard = res;
      })
    })
  }
}
