import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Accomodation } from '../models/accomodation.model';

@Injectable({
  providedIn: 'root'
})
export class AccomodationService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private http: HttpClient) { }

  getAccomodation(id: string): Observable<Accomodation> {
    return this.http.get<Accomodation>(`api/accomodation-service/accomodation/${id}`);
  }

  getAccomodations(): Observable<Accomodation[]> {
    const url = 'api/accomodation-service/accomodation';

    return this.http
      .get<Accomodation[]>(url, this.httpOptions);
  }

  getAccomodationsForHost(hostId: string): Observable<Accomodation[]> {
    const url = 'api/accomodation-service/accomodation/host/' + hostId;

    return this.http
      .get<Accomodation[]>(url, this.httpOptions);
  }

  createAccomodation(accomodation: Accomodation){
    const url = '/api/accomodation-service/accomodation';

    return this.http
      .post<Accomodation>(url, accomodation, this.httpOptions);
  }

}
