import { Component } from '@angular/core';
import { RecommendedAccommodation } from '../models/recommended-accommodation.model';
import { RecommendedAccommodationService } from '../services/recommended-accommodation.service';
import { AccommodationDisplayDialogComponent } from '../../search/accommodation-display-dialog/accommodation-display-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-recommendations',
  templateUrl: './recommendations.component.html',
  styleUrls: ['./recommendations.component.scss']
})
export class RecommendationsComponent {

  recommendedAccommodations: RecommendedAccommodation[] = [];

  constructor(private recommendationService: RecommendedAccommodationService, private matDialog: MatDialog) { }

  ngOnInit() {
    this.recommendationService.get().subscribe((res) => this.recommendedAccommodations = res);
  }

  openAccommodationDisplay(id: string) {
    this.matDialog.open(AccommodationDisplayDialogComponent, {
      data: { id: id },
      width: '80%',
      height: '90%'
    });
  }

}
