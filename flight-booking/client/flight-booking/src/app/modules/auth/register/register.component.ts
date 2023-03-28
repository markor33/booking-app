import { Component } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { RegisterRequest } from '../model/register-request';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  hidePassword: boolean = true;
  registerRequest: RegisterRequest = new RegisterRequest();

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar) { }

  register(): void {
    this.authService.register(this.registerRequest).subscribe((res) => {
      this.snackBar.open('Registration is successful!', 'Ok', {
        duration: 3000
      }).afterDismissed().subscribe(() => this.router.navigate(['auth/login']));
    },
    (error) => {
      this.snackBar.open('Registration not successful!', 'Ok', {
        duration: 3000,
        panelClass: ['warn']
      })
    });
  }

}
