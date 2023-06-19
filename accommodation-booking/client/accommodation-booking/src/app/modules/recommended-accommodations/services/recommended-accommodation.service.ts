import { Injectable } from '@angular/core';
import { RecommendedAccommodation } from '../models/recommended-accommodation.model';
import { Observable } from 'rxjs/internal/Observable';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RecommendedAccommodationService {

  constructor(private httpClient: HttpClient) { }

  get(): Observable<RecommendedAccommodation[]> {
    return this.httpClient.get<RecommendedAccommodation[]>('api/accommodation-recommendation/accommodation');
  }

}
