import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminFlightsComponent } from './admin-flights/admin-flights.component';
import { RouterModule, Routes } from '@angular/router';
import {MatCardModule} from '@angular/material/card';
import { FlightInfComponent } from './admin-flights/flight-inf/flight-inf.component';
import {MatDialogModule} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatOptionModule } from '@angular/material/core';
import { FormsModule } from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';


const routes: Routes = [
  { path: 'flights/admin', component: AdminFlightsComponent}
];

@NgModule({
  declarations: [
    AdminFlightsComponent,
    FlightInfComponent,
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatDialogModule,
    MatFormFieldModule,
    MatOptionModule,
    MatSelectModule,
    FormsModule,
    RouterModule.forChild(routes),
  ]
})
export class FlightsModule { }
