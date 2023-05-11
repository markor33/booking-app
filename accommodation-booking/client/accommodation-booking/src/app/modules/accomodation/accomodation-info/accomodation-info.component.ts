import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Accomodation } from '../models/accomodation.model';

@Component({
  selector: 'app-accomodation-info',
  templateUrl: './accomodation-info.component.html',
  styleUrls: ['./accomodation-info.component.scss']
})
export class AccomodationInfoComponent implements OnInit{

  accomodation: Accomodation;

  constructor(private router: Router) {
    this.accomodation = this.router.getCurrentNavigation()?.extras.state?.['accomodation'];
  }

  ngOnInit(): void {
    
  }

}
