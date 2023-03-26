import { Component } from '@angular/core';
import { FlightService } from '../service/flight.service';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent {

  constructor(private flightService: FlightService) { }

  ngOnInit(): void {
    this.flightService.getFlights().subscribe((res) => {
      console.log(res);
    })
  }

}
