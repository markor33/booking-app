import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReservationRequestsComponent } from './reservation-requests/reservation-requests.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/helpers/auth.guard';
import { RoleGuard } from '../auth/helpers/role.guard';

const routes: Routes = [
  {path: 'requests', component: ReservationRequestsComponent, canActivate: [AuthGuard, RoleGuard], data: {roles: ['HOST']}}
];

@NgModule({
  declarations: [
    ReservationRequestsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ]
})
export class ReservationModule { }
