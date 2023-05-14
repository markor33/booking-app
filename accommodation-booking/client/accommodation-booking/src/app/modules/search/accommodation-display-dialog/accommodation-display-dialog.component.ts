import { Component, Inject } from '@angular/core';
import { AccomodationService } from '../../accomodation/services/accomodation.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Accomodation } from '../../accomodation/models/accomodation.model';

@Component({
  selector: 'app-accommodation-display-dialog',
  templateUrl: './accommodation-display-dialog.component.html',
  styleUrls: ['./accommodation-display-dialog.component.scss']
})
export class AccommodationDisplayDialogComponent {

  accommodation: Accomodation = new Accomodation();

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private accommodationService: AccomodationService) { 
      this.accommodationService.getAccomodation(data.id as string).subscribe((res) => this.accommodation = res);
  }

  getBenefitNames(): string {
    return this.accommodation.benefits.map(benefit => benefit.name).join(', ');
  }

}
