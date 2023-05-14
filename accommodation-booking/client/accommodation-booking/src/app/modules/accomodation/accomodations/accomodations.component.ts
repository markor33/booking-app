import { Component, OnInit } from '@angular/core';
import { AccomodationService } from '../services/accomodation.service';
import { Accomodation } from '../models/accomodation.model';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-accomodations',
  templateUrl: './accomodations.component.html',
  styleUrls: ['./accomodations.component.scss']
})
export class AccomodationsComponent implements OnInit{

  accomodations: Accomodation[];

  constructor(private accomodationService: AccomodationService, private authService: AuthService){
    this.accomodations = [];
  }

  ngOnInit(): void {  
    this.getAccomodationsForHost(this.authService.getUserId());
  }

  getAccomodationsForHost(hostId: string){
    this.accomodationService.getAccomodationsForHost(hostId).subscribe( (res) => {
      this.accomodations = res;
    });
  }

}
