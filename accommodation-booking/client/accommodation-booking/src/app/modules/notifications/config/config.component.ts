import { Component } from '@angular/core';
import { NotificationsConfigService } from '../services/notifications-config.service';

@Component({
  selector: 'app-config',
  templateUrl: './config.component.html',
  styleUrls: ['./config.component.scss']
})
export class ConfigComponent {

  configs: { [key: string]: boolean } = {
    'ReservationRequestCreatedNotification': true,
    'ReservationCanceledNotification': true,
    'ReservationRequestStatusChangedNotification': true,
    'HostReviewedNotification': true,
    'HostProminentStatusChanged': true
  };

  constructor(private notificationConfigService: NotificationsConfigService) {
    
  }

  ngOnInit() {
    this.notificationConfigService.get().subscribe(
      (res) => this.configs = res,
      (err) => console.log(err)
    );
  }

  save() {
    this.notificationConfigService.update(this.configs).subscribe((res) => console.log(res));
  }

  updateConfigValue(key: string, value: boolean) {
    this.configs[key] = value;
  }

}
