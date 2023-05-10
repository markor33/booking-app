import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReservationRequest } from '../model/reservation-request.model';

@Injectable({
  providedIn: 'root'
})
export class ReservationRequestService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private httpClient: HttpClient) { }

  getRequestByUser(): Observable<ReservationRequest[]>{
    return this.httpClient.get<ReservationRequest[]>('api/reservations-service/reservationrequest/user', this.httpOptions);
  }
  approveRequest(id: string): Observable<boolean>{
    return this.httpClient.put<boolean>('api/reservations-service/reservationrequest/approve/'+ id, this.httpOptions);
  }
  declineRequest(id: string): Observable<boolean>{
    return this.httpClient.put<boolean>('api/reservations-service/reservationrequest/decline/'+ id, this.httpOptions);
  }
  deleteRequest(id: string): Observable<boolean>{
    return this.httpClient.delete<boolean>('api/reservations-service/reservationrequest/' + id, this.httpOptions);
  }
}