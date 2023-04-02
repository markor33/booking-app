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
  numberOfTickets: number[];
  temp: boolean = true;

  constructor(private flightService: FlightService, private dialog: MatDialog, private router: Router){ 
    this.flights = [];
    this.bookedFlights = [];
    this.numberOfTickets = [];
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
            this.numberOfTickets.push(e.numberOfTickets);
          })
        }        
      }
    }
  }

  ngOnInit(): void{

  }

  flightInfo(flight: Flight, i: number){
    const dialogRef = this.dialog.open(FlightInfComponent,{
      data: { data: flight, temp: this.temp, tickets: this.numberOfTickets[i] }
    }).afterClosed()
    .subscribe((shouldReload: boolean) => { 
      if(this.temp) {
        this.flightService.getAllFlights().subscribe((res) => {
          this.flights = res;
        }) 
      }
    });
  }
}
