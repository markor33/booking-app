import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { SearchQuery } from '../models/search-query.model';
import { Accommodation } from '../models/accommodation.model';

@Injectable({
  providedIn: 'root'
})
export class AccommodationSearchService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private httpClient: HttpClient) { }

  search(query: SearchQuery): Observable<Accommodation[]> {
    return this.httpClient.post<Accommodation[]>('api/aggregator/accommodation/search', query, this.httpOptions);
  }

}
