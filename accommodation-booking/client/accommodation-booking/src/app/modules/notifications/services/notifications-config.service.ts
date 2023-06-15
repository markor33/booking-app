import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationsConfigService {

  constructor(private httpClient: HttpClient) { }

  get(): Observable<any> {
    return this.httpClient.get(`api/notifications-service/config`);
  }

  update(notificationsConfig: any): Observable<any> {
    return this.httpClient.put(`api/notifications-service/config`, notificationsConfig);
  }

}
