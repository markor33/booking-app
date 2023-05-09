import { Component } from '@angular/core';
import { ApplicationUserService } from '../service/application-user.service';
import { Credentials } from '../model/credentials.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  credentials: Credentials;

  constructor(private userService: ApplicationUserService){
    this.credentials = {
      email: "",
      userName: "host",
      oldPassword: "12345",
      newPassword: "123"
    }
  }

  changeCredentials(){
    this.userService.changeCredentials(this.credentials).subscribe((res) => {
      console.log(true);
    })
  }
}
