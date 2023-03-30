import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Flight } from '../../model/flight.model';
import { FlightService } from '../../service/flight.service';

@Component({
  selector: 'app-create-flight',
  templateUrl: './create-flight.component.html',
  styleUrls: ['./create-flight.component.css']
})
export class CreateFlightComponent{

  flight: Flight;
  base64textString: any;
  constructor(private flightService: FlightService,
    private router: Router){

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

  createFlight(){
    this.flightService.createFlight(this.flight).subscribe((res) => {
      this.router.navigate(['/flights/admin']);
    })
  }

  handleUpload(event : any) {
    let reader = new FileReader();
    const file = event.target.files[0];
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.flight.imgUrl = reader.result as string;
  }; 
}
}
