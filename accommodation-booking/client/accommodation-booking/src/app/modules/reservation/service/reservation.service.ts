import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Reservation } from '../model/reservation.model';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private httpClient: HttpClient) { }

  getNumberOfCancelationforGuest(id: string): Observable<number>{
    return this.httpClient.get<number>('api/reservations-service/reservation/canceled/reservations/'+ id, this.httpOptions);
  }
  getByReservation(): Observable<Reservation[]>{
    return this.httpClient.get<Reservation[]>('api/aggregator/reservation/user', this.httpOptions);
  }
  cancelReservation(id: string): Observable<boolean>{
    return this.httpClient.put<boolean>('api/reservations-service/reservation/'+ id, this.httpOptions);
  }
}
