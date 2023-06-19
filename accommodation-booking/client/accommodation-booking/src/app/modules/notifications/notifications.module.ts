import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ConfigComponent } from './config/config.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';



@NgModule({
  declarations: [
    ConfigComponent
  ],
  imports: [
    CommonModule,
    MatCheckboxModule,
    FormsModule,
    MatButtonModule,
    MatSnackBarModule
  ]
})
export class NotificationsModule { }
