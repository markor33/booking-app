import { Component } from '@angular/core';
import { AuthService } from '../../auth/service/auth.service';
import { Router } from '@angular/router';
import { FlightService } from '../../flights/service/flight.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  isUserLogged: boolean = false;
  userRole: string = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private flightService: FlightService) {
  }

  ngOnInit(): void {
    this.authService.loginObserver.subscribe((val) => {
      this.isUserLogged = val;
      if(this.isUserLogged)
        this.userRole = this.authService.getUserRole();
    });
  }

  logout(): void {
    this.authService.logout();
    this.userRole = '';
    this.router.navigate(['/']);
  }

  getFlights(): void {
    this.flightService.getAllFlights().subscribe((res) => {
      this.router.navigateByUrl('/flights/admin', {state: {flights: res}});
    })
  }

  getMyFlights(): void {
    this.flightService.getUserFlights().subscribe((res) => {
      this.router.navigateByUrl('/flights/user', {state: {bookedFlights: res}});
    })
  }

}
