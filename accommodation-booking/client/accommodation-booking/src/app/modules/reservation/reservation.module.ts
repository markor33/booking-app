import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReservationRequestsComponent } from './reservation-requests/reservation-requests.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/helpers/auth.guard';
import { RoleGuard } from '../auth/helpers/role.guard';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { ReservationsComponent } from './reservations/reservations.component';
const routes: Routes = [
  {path: 'requests', component: ReservationRequestsComponent, canActivate: [AuthGuard, RoleGuard], data: {roles: ['HOST','GUEST']}},
  {path: 'reservations', component: ReservationsComponent, canActivate: [AuthGuard, RoleGuard], data: {roles: ['HOST','GUEST']}},
];

@NgModule({
  declarations: [
    ReservationRequestsComponent,
    ReservationsComponent
  ],
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatTableModule,
    RouterModule.forChild(routes),
  ]
})
export class ReservationModule { }
