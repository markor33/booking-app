import { Component, Input, OnInit } from '@angular/core';
import { Accomodation } from '../models/accomodation.model';
import { PriceType } from '../models/accomodation.model';

@Component({
  selector: 'app-accomodation-card',
  templateUrl: './accomodation-card.component.html',
  styleUrls: ['./accomodation-card.component.scss']
})
export class AccomodationCardComponent implements OnInit{
  @Input() accomodation: Accomodation;
  imageUrl: string = '';

  constructor(){
    this.accomodation = new Accomodation;
  }

  ngOnInit(){
    this.imageUrl = this.accomodation.photos[0].url;
  }
}
