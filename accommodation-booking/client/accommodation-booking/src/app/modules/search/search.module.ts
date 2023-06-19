import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccommodationSearchComponent } from './accommodation-search/accommodation-search.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import {MatInputModule} from '@angular/material/input';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import {MatButtonModule} from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { AccommodationDisplayDialogComponent } from './accommodation-display-dialog/accommodation-display-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { RecommendedAccommodationsModule } from '../recommended-accommodations/recommended-accommodations.module';
import { MatSelectModule } from '@angular/material/select';

const routes: Routes = [
  { path: '', component: AccommodationSearchComponent },
];

@NgModule({
  declarations: [
    AccommodationSearchComponent,
    AccommodationDisplayDialogComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    FormsModule,
    MatIconModule,
    MatDividerModule,
    MatSelectModule,
    MatDialogModule,
    RecommendedAccommodationsModule,
    RouterModule.forChild(routes),
  ],
  exports: [
    AccommodationSearchComponent
  ]
})
export class SearchModule { }
