import { Component } from '@angular/core';
import { Accomodation, PriceType } from '../models/accomodation.model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BenefitService } from '../services/benefit.service';
import { Benefit } from '../models/benefit.model';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-create-accomodation',
  templateUrl: './create-accomodation.component.html',
  styleUrls: ['./create-accomodation.component.scss']
})
export class CreateAccomodationComponent {

  accomodation: Accomodation;
  benefits: Benefit[];
  selectedBenefits: Benefit[]

  constructor(private benefitService: BenefitService, private authService: AuthService) { 
    this.accomodation = new Accomodation();
    this.benefits = [];
    this.selectedBenefits = [];
  }

  ngOnInit(): void {
    this.getBenefits();
  }

  create(): void {
    this.accomodation.id = '';
    this.accomodation.hostId = this.authService.getUserId();
    this.accomodation.benefits = this.selectedBenefits;
    console.log(this.accomodation);
  }

  getBenefits() {
    this.benefitService.getBenefits().subscribe( (res) => {
      this.benefits = res;
    });
  }

}
