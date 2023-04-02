import { HttpClient, HttpBackend  } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { BookedFlight } from '../model/booked-flight.model';
import { Flight } from '../model/flight.model';

@Injectable({
  providedIn: 'root'
})
export class FlightService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  http: HttpClient;

  constructor(private httpClient: HttpClient, handler: HttpBackend) {
    this.http = new HttpClient(handler)
   }

  getAllFlights() : Observable<Flight[]>{
    return this.httpClient.get<Flight[]>('api/flight', this.httpOptions)
    .pipe(catchError(this.handleError<Flight[]>('getAllFlights', [])));
  }

  getUserFlights() : Observable<Flight[]>{
    return this.httpClient.get<Flight[]>('api/BookedFlight/user', this.httpOptions)
    .pipe(catchError(this.handleError<Flight[]>('getUserFlights', [])));
  }

  deleteFlight(id: string) : Observable<boolean>{
    return this.httpClient.delete<boolean>('api/flight/' + id, this.httpOptions);
  }

  bookFlight(bookedFlight: BookedFlight) : Observable<boolean>{
    return this.httpClient.post<boolean>('api/BookedFlight', bookedFlight, this.httpOptions)
    .pipe(catchError(this.handleError<boolean>('bookFlight')));
  }
  
  createFlight(flight: Flight) : Observable<Flight>{
    return this.httpClient.post<Flight>('api/flight', flight, this.httpOptions)
    .pipe(catchError(this.handleError<Flight>('createFlight')));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}


