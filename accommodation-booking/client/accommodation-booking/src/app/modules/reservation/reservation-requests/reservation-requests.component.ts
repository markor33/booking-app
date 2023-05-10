import { Component, OnInit } from '@angular/core';
import { ReservationRequestService } from '../service/reservation-request.service';
import { ReservationRequest } from '../model/reservation-request.model';

@Component({
  selector: 'app-reservation-requests',
  templateUrl: './reservation-requests.component.html',
  styleUrls: ['./reservation-requests.component.scss']
})
export class ReservationRequestsComponent implements OnInit {

   requests: ReservationRequest[];

  constructor(private reservationRequestService: ReservationRequestService){ 
    this.requests = [];
  }
  ngOnInit(): void {
    this.getByUser();
  }
  approveRequest(id: string){
    this.reservationRequestService.approveRequest(id).subscribe();
  }
  declineRequest(id: string){
    this.reservationRequestService.declineRequest(id).subscribe();
  }
  getByUser(){
    this.reservationRequestService.getRequestByUser().subscribe((res) =>{
      this.requests = res;
      console.log(res)
    });
  }

}
