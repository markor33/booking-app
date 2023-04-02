import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import {MatCardModule} from '@angular/material/card';
import { FlightInfComponent } from './flight-inf/flight-inf.component';
import {MatDialogModule} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatOptionModule } from '@angular/material/core';
import { FormsModule } from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import { CreateFlightComponent } from './create-flight/create-flight.component';
import { AuthGuard } from '../auth/helpers/auth.guard';
import { RoleGuard } from '../auth/helpers/role.guard';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { FlightsListComponent } from './flights-list/flights-list.component';

const routes: Routes = [
  { path: 'flights', component: FlightsListComponent,  canActivate: [AuthGuard, RoleGuard], data: { roles: ['ADMIN', 'USER'], flights: []}},
  { path: 'flights/create', component: CreateFlightComponent,  canActivate: [AuthGuard, RoleGuard], data: { roles: ['ADMIN']}}
];

@NgModule({
  declarations: [
    FlightsListComponent,
    FlightInfComponent,
    CreateFlightComponent,
    FlightsListComponent,
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatDialogModule,
    MatFormFieldModule,
    MatOptionModule,
    MatSelectModule,
    FormsModule,
    MatButtonModule,
    MatInputModule,
    MatSnackBarModule,
    RouterModule.forChild(routes),
  ]
})
export class FlightsModule { }
