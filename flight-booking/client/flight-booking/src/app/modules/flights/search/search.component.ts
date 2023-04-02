import { Component } from '@angular/core';
import { FlightService } from '../service/flight.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {

  date: Date = new Date();
  origin: string = '';
  destination: string = '';
  numberOfPassengers: number = 0;

  constructor(private flightService: FlightService, private router: Router) {

  }

  ngOnInit() {
    
  }

  search() {
    this.flightService.search(this.date, this.origin, this.destination, this.numberOfPassengers).subscribe((res) => {
      this.router.navigateByUrl('/flights/admin', {state: {flights: res}});
    });
  }

}
