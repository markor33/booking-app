import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import * as signalR from '@microsoft/signalr';
import { AuthService } from '../../auth/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Notification } from '../models/notification.model';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  connection!: signalR.HubConnection;

  constructor(
    private authService: AuthService,
    private jwtHelper: JwtHelperService,
    private snackBar: MatSnackBar) { 
      this.authService.loginObserver.subscribe((isLoogedIn) => {
        if (!isLoogedIn) {
          this.connection?.stop();
          return;
        }
        this.configureConnection();
      });
  }

  private configureConnection() {
    const userId = this.authService.getUserId()
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`hub/notifications?userId=${userId}`, { accessTokenFactory: () => this.jwtHelper.tokenGetter()})
      .build();

    this.connection.start()
    .then(() => console.log('OK'))
    .catch((err) => console.log(err));

    this.connection.on('newNotification', (notification: Notification) => {
      this.snackBar.open(notification.text, undefined, {
        duration: 3000
      })
    });

  }
}
