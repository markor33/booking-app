import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/services/auth.service';
import { NotificationsService } from '../../notifications/services/notifications.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfigComponent } from '../../notifications/config/config.component';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent{

  isUserLogged: boolean = false;
  userRole: string = '';
  
  constructor(
    private authService: AuthService,
    private dilaog: MatDialog,
    private notificationService: NotificationsService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.authService.loginObserver.subscribe((val) => {
      this.isUserLogged = val;
      if(this.isUserLogged)
        this.userRole = this.authService.getUserRole();
    });
  }

  openNotificationsSettings() {
    this.dilaog.open(ConfigComponent, {
      width: '40%',
      height: '50%'
    });
  }

  logout(): void {
    this.authService.logout();
    this.userRole = '';
    this.router.navigate(['/']);
  }
}
