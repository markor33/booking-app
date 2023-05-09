import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './profile/profile.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/helpers/auth.guard';
import { RoleGuard } from '../auth/helpers/role.guard';

const routes: Routes = [
  {path: 'profile', component: ProfileComponent, canActivate: [AuthGuard, RoleGuard], data: {roles: ['HOST', 'GUEST']}}
];

@NgModule({
  declarations: [
    ProfileComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ]
})
export class UserModule { }
