import { Component } from '@angular/core';
import { Flight } from '../model/flight.model';
import { FlightService } from '../service/flight.service';
import { MatDialog } from '@angular/material/dialog';
import { FlightInfComponent } from '../flight-inf/flight-inf.component';
import { ActivatedRoute, Router } from '@angular/router';
import { BookedFlight } from '../model/booked-flight.model';

@Component({
  selector: 'app-flights-list',
  templateUrl: './flights-list.component.html',
  styleUrls: ['./flights-list.component.css']
})
export class FlightsListComponent {

  flights: Flight[];
  bookedFlights: BookedFlight[];
  temp: boolean = true;

  constructor(private flightService: FlightService, private dialog: MatDialog, private router: Router){ 
    this.flights = [];
    this.bookedFlights = [];
    var navigation = this.router.getCurrentNavigation();
    if(navigation != null){
      var extras = navigation.extras.state;
      if(extras != undefined) {
        this.flights = extras['flights'];
        if(this.flights == undefined){
          this.flights = [];
          this.bookedFlights = extras['bookedFlights'];
          this.temp = false;
          this.bookedFlights.forEach(e => {     
            this.flights.push(e.flight);
          })
        }        
      }
    }
  }

  ngOnInit(): void{

  }

  flightInfo(flight: Flight){
    const dialogRef = this.dialog.open(FlightInfComponent,{
      data: { data: flight, temp: this.temp }
    }).afterClosed()
    .subscribe((shouldReload: boolean) => { 
      
    });
  }
}
