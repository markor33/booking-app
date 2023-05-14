import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { PriceInterval } from '../models/price-interval.model';

@Injectable({
  providedIn: 'root'
})
export class PriceIntervalService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private http: HttpClient) { }

  getPriceIntervalsForAccomodation(accomodationId: string): Observable<PriceInterval[]> {
    const url = 'api/accomodation-service/priceInterval/accomodation/' + accomodationId;

    return this.http
      .get<PriceInterval[]>(url, this.httpOptions);
  }

  createPriceInterval(priceInterval: PriceInterval){
    const url = '/api/accomodation-service/priceInterval';

    return this.http
      .post<PriceInterval>(url, priceInterval, this.httpOptions);
  }

  editPriceInterval(priceInterval: PriceInterval){
    const url = '/api/accomodation-service/priceInterval/' + priceInterval.id;

    return this.http
      .put<PriceInterval>(url, priceInterval, this.httpOptions);
  }

}
