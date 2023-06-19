import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReservationRequest } from '../model/reservation-request.model';
import { Request } from '../../search/models/request.model';
import { convertToUTCDate } from '../../shared/utils/date-helper.util';

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
  createRequest(request: Request): Observable<ReservationRequest>{
    request.period.start = convertToUTCDate(request.period.start);
    request.period.end = convertToUTCDate(request.period.end);
    return this.httpClient.post<ReservationRequest>('api/reservations-service/reservationrequest/', request, this.httpOptions);
  }
}
