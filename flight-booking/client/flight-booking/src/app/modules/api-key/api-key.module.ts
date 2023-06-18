import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiKeyComponent } from './api-key/api-key.component';
import { HttpClientModule } from '@angular/common/http';
import { Route, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatCheckboxModule} from '@angular/material/checkbox';

const routes: Route[] = [
  { path: 'api-key', component: ApiKeyComponent }
];

@NgModule({
  declarations: [
    ApiKeyComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    MatButtonModule,
    MatFormFieldModule,
    FormsModule,
    MatCheckboxModule,
    RouterModule.forChild(routes),
  ]
})
export class ApiKeyModule { }
