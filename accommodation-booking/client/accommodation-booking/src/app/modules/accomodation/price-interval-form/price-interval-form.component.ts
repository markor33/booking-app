import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PriceInterval } from '../models/price-interval.model';
import { Accomodation } from '../models/accomodation.model';
import { Price } from '../../reservation/model/price.model';
import { DateTimeRange } from '../models/date-time-range.model';
import { PriceIntervalService } from '../services/price-interval.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-price-interval-form',
  templateUrl: './price-interval-form.component.html',
  styleUrls: ['./price-interval-form.component.scss']
})
export class PriceIntervalFormComponent implements OnInit{

  interval: PriceInterval;
  accomodation: Accomodation;
  mode: string;

  constructor(private router: Router, private priceIntervalSevice: PriceIntervalService, private snackBar: MatSnackBar) {
    if(this.router.getCurrentNavigation()?.extras.state?.['mode'] == 'create') {
      this.mode = 'create';
    } else {
      this.mode = 'edit';
    }

    if(this.mode == 'create') {
      this.accomodation = this.router.getCurrentNavigation()?.extras.state?.['accomodation'];
      this.interval = new PriceInterval();
    } else {
      this.interval = this.router.getCurrentNavigation()?.extras.state?.['interval'];
      this.accomodation = this.router.getCurrentNavigation()?.extras.state?.['accomodation'];
    }

  }

  ngOnInit(): void {
    
  }

  save() {
    this.interval.accommodationId = this.accomodation.id;
    if(this.mode == 'create') {
      console.log(this.interval)
      this.priceIntervalSevice.createPriceInterval(this.interval).subscribe( () => {
        this.snackBar.open("Successfully created the special interval!", "Ok", {
          duration: 2000,
          panelClass: ['snack-bar']
        });
        this.router.navigate(['/accomodation/' + this.accomodation.id], { state: {accomodation: this.accomodation}});
      }, () => {
        this.snackBar.open("There was an error creating the special interval!", "Ok", {
          duration: 2000,
          panelClass: ['snack-bar']
        });
      })

    } else {
      this.priceIntervalSevice.editPriceInterval(this.interval).subscribe( () => {
        this.snackBar.open("Successfully edited the special interval!", "Ok", {
          duration: 2000,
          panelClass: ['snack-bar']
        });
        this.router.navigate(['/accomodation/' + this.accomodation.id], { state: {accomodation: this.accomodation}});
      }, () => {
        this.snackBar.open("There was an error editing the special interval!", "Ok", {
          duration: 2000,
          panelClass: ['snack-bar']
        });
      })

    }
  }

}
