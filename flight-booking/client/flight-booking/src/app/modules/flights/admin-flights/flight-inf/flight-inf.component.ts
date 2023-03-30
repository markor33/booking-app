import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Flight } from '../../model/flight.model';
import { FlightService } from '../../service/flight.service';

@Component({
  selector: 'app-flight-inf',
  templateUrl: './flight-inf.component.html',
  styleUrls: ['./flight-inf.component.css']
})
export class FlightInfComponent implements OnInit{

  flight: Flight;
  constructor(
    private dailog: MatDialogRef<FlightInfComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Flight,
    private flightService: FlightService,
  ){
    this.flight = {
      departureTime: new Date(),
      landingTime: new Date(),
      ticketPrice: 0,
      numOfAvailableTickets: 0,
      origin: "",
      destination: "",
      imgUrl: "",
      id:""
    }
  }
  
  ngOnInit(): void {
    this.flight = this.data;
  }

  deleteFlight(){
    this.flightService.deleteFlight(this.flight.id).subscribe((res) => 
    this.dailog.close())
  }
}
