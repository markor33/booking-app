import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { LoginRequest } from '../models/login-request';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  hidePassword: boolean = true;
  credentials: LoginRequest = new LoginRequest();

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar) { }
    
  login(): void {
    this.authService.login(this.credentials).subscribe({
      complete: () => this.router.navigate(['/']),
      error: (err) => {
        console.log(err);
        this.snackBar.open("Wrong username and/or password!", "Ok", {
          duration: 2000,
          panelClass: ['snack-bar']
        });
      }
    });
  }

}
