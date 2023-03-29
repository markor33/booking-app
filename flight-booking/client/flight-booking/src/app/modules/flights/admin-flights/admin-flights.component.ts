import { Component } from '@angular/core';
import { Flight } from '../model/flight.model';
import { FlightService } from '../service/flight.service';
import { MatDialog } from '@angular/material/dialog';
import { FlightInfComponent } from './flight-inf/flight-inf.component';

@Component({
  selector: 'app-admin-flights',
  templateUrl: './admin-flights.component.html',
  styleUrls: ['./admin-flights.component.css']
})
export class AdminFlightsComponent {

  flights: Flight[];

  constructor(private flightService: FlightService, private dialog: MatDialog){ 
    this.flights = [];
  }

  ngOnInit(): void{
    this.getAllFlights();

  }
  getAllFlights(){
    this.flightService.getAllFlights().subscribe((res: Flight[]) => {
      this.flights = res;
    })
  }
  flightInfo(flight: Flight){
    const dialogRef = this.dialog.open(FlightInfComponent,{
      width: '500px',
      height: '700px',
      data: flight
    }).afterClosed()
    .subscribe((shouldReload: boolean) => { this.getAllFlights();
    });
  }
}
