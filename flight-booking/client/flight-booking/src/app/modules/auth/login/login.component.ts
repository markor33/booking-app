import { Component } from '@angular/core';
import { LoginRequest } from '../model/login-request';
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  hidePassword: boolean = true;
  credentials: LoginRequest = new LoginRequest();

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar) { }

  login(): void {
    this.authService.login(this.credentials).subscribe(res => {
      this.router.navigate(['/']);
    },
    error => this.snackBar.open("Wrong username and/or password!", "Ok", {
      duration: 2000,
      panelClass: ['snack-bar']
    }));
  }

}
