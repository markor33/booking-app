import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../auth/service/auth.service';
import { BookedFlight } from '../model/booked-flight.model';
import { Flight } from '../model/flight.model';
import { FlightService } from '../service/flight.service';

@Component({
  selector: 'app-flight-inf',
  templateUrl: './flight-inf.component.html',
  styleUrls: ['./flight-inf.component.css']
})
export class FlightInfComponent implements OnInit{

  flight: Flight;
  bookedFlight: BookedFlight;
  isUserLogged: boolean = false;
  userRole: string = '';
  temp: boolean = false;

  constructor(
    private dailog: MatDialogRef<FlightInfComponent>,
    private authService: AuthService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private flightService: FlightService, private snackBar: MatSnackBar
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
    this.bookedFlight = {
      flightId: this.flight.id,
      flight: this.flight,
      numberOfTickets: 2
    }
  }
  
  ngOnInit(): void {
    this.authService.loginObserver.subscribe((val) => {
      this.isUserLogged = val;
      if(this.isUserLogged)
        this.userRole = this.authService.getUserRole();
    });
    this.flight = this.data.data;
    this.temp = this.data.temp;
    this.bookedFlight.flightId = this.data.id;
  }

  deleteFlight(){
    this.flightService.deleteFlight(this.flight.id).subscribe((res) => {
      this.snackBar.open("Flight successfully deleated!", "Ok", {
        duration: 2000,
        panelClass: ['blue-snackbar']
      });
      setTimeout(() => {
        this.dailog.close()
      }, 1000);
    }
    )
  }

  bookFlight(){
    this.flightService.bookFlight(this.bookedFlight).subscribe(() => {
      this.snackBar.open("Flight successfully booked!", "Ok", {
        duration: 2000,
        panelClass: ['blue-snackbar']
      });
      setTimeout(() => {
        this.dailog.close()
      }, 1000);
    }
    )
  }
}
