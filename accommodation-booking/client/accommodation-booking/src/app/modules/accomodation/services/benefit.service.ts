import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Accomodation } from '../models/accomodation.model';
import { Benefit } from '../models/benefit.model';

@Injectable({
  providedIn: 'root'
})
export class BenefitService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private http: HttpClient) { }

  getBenefits(): Observable<Benefit[]> {
    const url = 'api/accomodation-service/benefit';

    return this.http
      .get<Benefit[]>(url, this.httpOptions);
  }
}
