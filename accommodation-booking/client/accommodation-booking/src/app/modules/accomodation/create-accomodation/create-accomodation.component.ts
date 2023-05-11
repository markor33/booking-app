import { Component } from '@angular/core';
import { Accomodation, PriceType } from '../models/accomodation.model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BenefitService } from '../services/benefit.service';
import { Benefit } from '../models/benefit.model';
import { AuthService } from '../../auth/services/auth.service';
import { Photo } from '../models/photo.model';
import { AccomodationService } from '../services/accomodation.service';

@Component({
  selector: 'app-create-accomodation',
  templateUrl: './create-accomodation.component.html',
  styleUrls: ['./create-accomodation.component.scss']
})
export class CreateAccomodationComponent {

  accomodation: Accomodation;
  benefits: Benefit[];
  selectedBenefits: Benefit[]
  photos: Photo[];

  constructor(private benefitService: BenefitService, private authService: AuthService, private accomodationService: AccomodationService) { 
    this.accomodation = new Accomodation();
    this.benefits = [];
    this.selectedBenefits = [];
    this.photos = [];
  }

  ngOnInit(): void {
    this.getBenefits();
  }

  create(): void {
    this.accomodation.id = '';
    this.accomodation.hostId = this.authService.getUserId();
    this.accomodation.benefits = this.selectedBenefits;
    this.accomodation.photos = this.photos;
    this.createAccomodation();
  }

  createAccomodation() {
    this.accomodationService.createAccomodation(this.accomodation).subscribe( (res) => {
      //redirect + snack bar
    })
  }

  getBenefits() {
    this.benefitService.getBenefits().subscribe( (res) => {
      this.benefits = res;
    });
  }

  handleUpload(event : any) {
    let reader = new FileReader();
    const file = event.target.files[0];
    reader.readAsDataURL(file);
    
    reader.onload = () => {
      const photo = {
        id: '',
        url: reader.result as string
      }
      this.photos.push(photo);

  }; 
}

}
