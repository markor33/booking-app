import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecommendationsComponent } from './recommendations/recommendations.component';
import { RouterModule, Routes } from '@angular/router';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';

const routes: Routes = [
  { path: 'recommended-appoitments', component: RecommendationsComponent }
];

@NgModule({
  declarations: [
    RecommendationsComponent
  ],
  imports: [
    CommonModule,
    MatDividerModule,
    MatIconModule,
    MatDialogModule,
    RouterModule.forChild(routes),
  ],
  exports: [
    RecommendationsComponent
  ]
})
export class RecommendedAccommodationsModule { }
