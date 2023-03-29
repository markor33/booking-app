import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Flight } from '../../model/flight.model';
import { FlightService } from '../../service/flight.service';

@Component({
  selector: 'app-create-flight',
  templateUrl: './create-flight.component.html',
  styleUrls: ['./create-flight.component.css']
})
export class CreateFlightComponent {

  flight: Flight;
  data: FormData;
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
    this.data = new FormData;
  }

  createFlight(){
    this.data.append('file', this.base64textString);
    this.data.append('upload_preset','dypot2fv');
    this.data.append('cloud_name','disvuvajt');
    this.data.append('public_id', this.flight.destination + Date.now());

    this.flightService.uploadSignature(this.data).subscribe((imageData) => {
      this.flight.imgUrl = imageData.secure_url;
      
      this.flightService.createFlight(this.flight).subscribe((res) => {
        this.router.navigate(['/flights/admin']);
      })

    })
  }

  handleUpload(event : any) {
    let reader = new FileReader();
    const file = event.target.files[0];
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.base64textString = reader.result;
  };
}
}
