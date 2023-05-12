import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccomodationsComponent } from './accomodations/accomodations.component';
import { RouterModule, Routes } from '@angular/router';
import { AccomodationCardComponent } from './accomodation-card/accomodation-card.component';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatRadioModule } from '@angular/material/radio';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatGridListModule } from '@angular/material/grid-list';
import { RoleGuard } from '../auth/helpers/role.guard';
import { AuthGuard } from '../auth/helpers/auth.guard';
import { CreateAccomodationComponent } from './create-accomodation/create-accomodation.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import { AccomodationInfoComponent } from './accomodation-info/accomodation-info.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { MatTableModule } from '@angular/material/table';
import { PriceIntervalFormComponent } from './price-interval-form/price-interval-form.component';
import { MatDatepicker, MatDatepickerModule } from '@angular/material/datepicker';

const routes: Routes = [
  { path: 'accomodation', component: AccomodationsComponent, canActivate: [AuthGuard, RoleGuard], data: { roles: ['HOST'] }, },
  { path: 'accomodation/create', component: CreateAccomodationComponent, canActivate: [AuthGuard, RoleGuard], data: { roles: ['HOST'] }, },
  { path: 'accomodation/:id', component: AccomodationInfoComponent, canActivate: [AuthGuard, RoleGuard], data: {roles: ['HOST']} },
  { path: 'accomodation/:id/price-interval', component: PriceIntervalFormComponent, canActivate: [AuthGuard, RoleGuard], data: {roles: ['HOST']} }
];

@NgModule({
  declarations: [
    AccomodationsComponent,
    AccomodationCardComponent,
    CreateAccomodationComponent,
    AccomodationInfoComponent,
    PriceIntervalFormComponent
  ],
  imports: [
    CarouselModule,
    CommonModule,
    MatCardModule,
    MatInputModule,
    MatTableModule,
    MatIconModule,
    MatDatepickerModule,
    MatButtonModule,
    MatSnackBarModule,
    FormsModule,
    MatDividerModule,
    MatCheckboxModule,
    MatRadioModule,
    MatGridListModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class AccomodationModule { }
