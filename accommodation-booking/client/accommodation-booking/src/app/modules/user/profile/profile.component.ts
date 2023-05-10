import { Component, OnInit } from '@angular/core';
import { ApplicationUserService } from '../service/application-user.service';
import { Credentials } from '../model/credentials.model';
import { UserProfile } from '../model/user-profile.model';
import { Address } from '../model/address.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  credentials: Credentials;
  userProfile: UserProfile;
  address: Address;

  constructor(private userService: ApplicationUserService, private snackBar: MatSnackBar,
    private authService: AuthService,
    private router: Router){
    this.credentials = {
      email: "",
      userName: "",
      oldPassword: "",
      newPassword: ""
    }
    this.address = {
      country: "",
      city: "",
      street: "",
      number: ""
    }
    this.userProfile = {
      email: "",
      firstName: "",
      lastName: "",
      address: this.address
    }
  }
  ngOnInit(): void {
    this.getUserProfile();
  }

  changeCredentials(){
    this.userService.changeCredentials(this.credentials).subscribe({
      complete: () =>{
        this.snackBar.open("Credentials successfully changed!", "Ok", {
          duration: 2000,
          panelClass: ['blue-snackbar']
        });
      }
    });
  }
  editProfile(){
    this.userService.editProfile(this.userProfile).subscribe({
      complete:() =>{
        this.snackBar.open("Profile is edited!", "Ok", {
          duration: 2000,
          panelClass: ['blue-snackbar']
        });
      }
    });
  }
  getUserProfile(){
    this.userService.getUserProfile().subscribe((res) => {
      this.userProfile = res;
    })
  }
  deleteProfile(){
    this.userService.deleteProfile().subscribe({
      complete:() =>{
        this.snackBar.open("Profile is deleted!", "Ok", {
          duration: 2000,
          panelClass: ['blue-snackbar']
        });

        this.authService.logout();
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.log(err);
        this.snackBar.open("You have active reservations, cant't delete profile", "Ok", {
          duration: 2000,
          panelClass: ['red-snackbar']
        });
      }
    });
  }
}
