import { Component } from '@angular/core';
import { RegisterRequest } from '../models/register-request';
import { AuthService } from '../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  hidePassword: boolean = true;

  registerRequest: RegisterRequest = new RegisterRequest();

  constructor(
    private router: Router,
    private authService: AuthService,
    private snackBar: MatSnackBar) {}

  register() {
    this.authService.register(this.registerRequest).subscribe({
      complete: this.registerCompletedSuccessfully.bind(this),
      error: (err) => this.snackBar.open('Register failed', 'Ok', { duration: 3000 }) 
    });
  }

  registerCompletedSuccessfully() {
    const snackBar = this.snackBar.open('Register successful', 'Ok', {
      duration: 3000
    });
    snackBar.afterDismissed().subscribe(() => this.router.navigate(['/auth/login']));
  }
  
}
