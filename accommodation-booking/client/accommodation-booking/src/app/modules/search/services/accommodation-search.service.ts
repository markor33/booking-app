import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Accommodation } from '../models/accommodation.model';
import { Observable } from 'rxjs/internal/Observable';
import { SearchQuery } from '../models/search-query.model';

@Injectable({
  providedIn: 'root'
})
export class AccommodationSearchService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private httpClient: HttpClient) { }

  search(query: SearchQuery): Observable<Accommodation[]> {
    return this.httpClient.post<Accommodation[]>('api/accommodation-search/search', query, this.httpOptions);
  }

  checkAvailability(checkAvailabilityArgs: any): Observable<Accommodation> {
    return this.httpClient.post<Accommodation>('api/accommodation-search/search/availability', checkAvailabilityArgs, this.httpOptions);
  }

}
