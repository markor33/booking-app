import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Flight } from '../model/flight.model';
import { BookedFlight } from '../model/booked-flight.model';
import { AuthService } from '../../auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class FlightBookingService {

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService) { }

  search(date: Date, origin: string, destination: string, numberOfPassengers: number) : Observable<Flight[]> {
    let url: string = `http://localhost:50000/api/Flight/search?date=${date}&origin=${origin}&destination=${destination}&numberOfPassengers=${numberOfPassengers}`;
    return this.httpClient.get<Flight[]>(url);
  }

  bookFlight(bookedFlight: BookedFlight) : Observable<boolean> {
    const apiKey = this.authService.getFlightBookingApiKey();
    const headers = new HttpHeaders({
      'API-KEY': apiKey as string
    });
    return this.httpClient.post<boolean>('http://localhost:50000/api/BookedFlight', bookedFlight, { headers: headers });
  }
  
}
