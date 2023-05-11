import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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
}
