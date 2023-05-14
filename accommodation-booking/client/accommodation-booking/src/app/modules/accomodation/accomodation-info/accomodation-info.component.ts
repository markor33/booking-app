import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Accomodation } from '../models/accomodation.model';
import { PriceIntervalService } from '../services/price-interval.service';

@Component({
  selector: 'app-accomodation-info',
  templateUrl: './accomodation-info.component.html',
  styleUrls: ['./accomodation-info.component.scss']
})
export class AccomodationInfoComponent implements OnInit{

  accomodation: Accomodation;
  displayedColumns: string[] = ['start', 'end', 'amount', 'edit'];

  constructor(private router: Router, private priceIntervalService: PriceIntervalService) {
    this.accomodation = this.router.getCurrentNavigation()?.extras.state?.['accomodation'];
  }

  ngOnInit(): void {
    this.fetchPriceIntervals();
  }

  fetchPriceIntervals(){
    this.priceIntervalService.getPriceIntervalsForAccomodation(this.accomodation.id).subscribe((res) => {
      this.accomodation.priceIntervals = res;
    })
  }

  createInterval() {
    this.router.navigate(['/accomodation/' + this.accomodation.id + '/price-interval'], { state: {accomodation: this.accomodation, mode: 'create'}});
  }

  editInterval(index: number) {
    this.router.navigate(['/accomodation/' + this.accomodation.id + '/price-interval'], { state: {interval: this.accomodation.priceIntervals[index], accomodation: this.accomodation, mode: 'edit'}});
  }

}
