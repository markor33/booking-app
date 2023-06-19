import { Component, Inject } from '@angular/core';
import { AccomodationService } from '../../accomodation/services/accomodation.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AccomodationDialog } from '../models/accommodationDialog.model';


@Component({
  selector: 'app-accommodation-display-dialog',
  templateUrl: './accommodation-display-dialog.component.html',
  styleUrls: ['./accommodation-display-dialog.component.scss']
})
export class AccommodationDisplayDialogComponent {

  accommodation: AccomodationDialog = new AccomodationDialog();
  stars: number[] = [1, 2, 3, 4, 5];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private accommodationService: AccomodationService) { 
      this.accommodationService.getAccommodationDialog(data.accommId as string, data.hostId as string).subscribe((res) => {
        this.accommodation = res
      });
  }

  getBenefitNames(): string {
    return this.accommodation.benefits.map(benefit => benefit.name).join(', ');
  }

}
