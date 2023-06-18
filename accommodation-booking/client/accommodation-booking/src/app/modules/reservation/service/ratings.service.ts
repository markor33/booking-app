import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HostRating } from '../model/host-rating.model';
import { Observable } from 'rxjs';
import { AccommodationRating } from '../model/accommodation-rating.model';

@Injectable({
  providedIn: 'root'
})
export class RatingsService {
  
  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private httpClient: HttpClient){ }

  rateHost(request: HostRating, res: string): Observable<number>{
    return this.httpClient.post<number>('api/ratings/hostRating/' + res, request, this.httpOptions);
  }

  deleteHostRating(id: string): Observable<void>{
    return this.httpClient.delete<void>('api/ratings/hostRating/' + id, this.httpOptions);
  }

  rateAccommodation(request: AccommodationRating): Observable<AccommodationRating>{
    return this.httpClient.post<AccommodationRating>('api/ratings/accommodationRating/', request, this.httpOptions);
  }

  deleteAccommodationRate(id: string): Observable<void>{
    return this.httpClient.delete<void>('api/ratings/accommodationRating/' + id, this.httpOptions);
  }
}
