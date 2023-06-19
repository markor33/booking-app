import { Component } from '@angular/core';
import { Accomodation, PriceType } from '../models/accomodation.model';
import { BenefitService } from '../services/benefit.service';
import { Benefit } from '../models/benefit.model';
import { AuthService } from '../../auth/services/auth.service';
import { Photo } from '../models/photo.model';
import { AccomodationService } from '../services/accomodation.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

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

  constructor(private benefitService: BenefitService, private authService: AuthService, private accomodationService: AccomodationService, private router: Router, private snackBar: MatSnackBar) { 
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
    this.accomodationService.createAccomodation(this.accomodation).subscribe( () => {
      this.snackBar.open("Successfully created the accomodation!", "Ok", {
        duration: 2000,
        panelClass: ['snack-bar']
      });
      this.router.navigate(['/accomodation']);
    })
  }

  getBenefits() {
    this.benefitService.getBenefits().subscribe( (res) => {
      this.benefits = res;
    });
  }

  resetPhotos() {
    this.photos = [];
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
