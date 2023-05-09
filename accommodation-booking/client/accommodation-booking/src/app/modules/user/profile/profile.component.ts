import { Component } from '@angular/core';
import { ApplicationUserService } from '../service/application-user.service';
import { Credentials } from '../model/credentials.model';
import { UserProfile } from '../model/user-profile.model';
import { Address } from '../model/address.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {

  credentials: Credentials;
  userProfile: UserProfile;
  address: Address;

  constructor(private userService: ApplicationUserService){
    this.credentials = {
      email: "",
      userName: "host",
      oldPassword: "12345",
      newPassword: "123456"
    }
    this.address = {
      country: "Srbija",
      city: "Novi Sad",
      street: "Dunavska",
      number: "1"
    }
    this.userProfile = {
      email: "",
      firstName: "HostFirstName",
      lastName: "HostLastName",
      address: this.address
    }
  }

  changeCredentials(){
    this.userService.changeCredentials(this.credentials).subscribe();
  }
  editProfile(){
    this.userService.editProfile(this.userProfile).subscribe();
  }
  getUserProfile(){
    this.userService.getUserProfile().subscribe((res) => {
      console.log(res);
    })
  }
  deleteProfile(){
    this.userService.deleteProfile().subscribe();
  }
}
