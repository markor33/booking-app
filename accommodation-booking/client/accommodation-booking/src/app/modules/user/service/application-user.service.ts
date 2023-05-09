import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Credentials } from '../model/credentials.model';
import { UserProfile } from '../model/user-profile.model';

@Injectable({
  providedIn: 'root'
})
export class ApplicationUserService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private httpClient: HttpClient) { }

  changeCredentials(credentials: Credentials): Observable<boolean>{
    return this.httpClient.put<boolean>('api/identity/applicationuser/change/credentials', credentials, this.httpOptions)
  }
  deleteProfile(): Observable<boolean>{
    return this.httpClient.delete<boolean>('api/identity/applicationuser', this.httpOptions)
  }
  editProfile(userProfile: UserProfile): Observable<boolean>{
    return this.httpClient.put<boolean>('api/identity/applicationuser/edit/profile', userProfile, this.httpOptions)
  }
  getUserProfile(): Observable<UserProfile>{
    return this.httpClient.get<UserProfile>('api/identity/applicationuser', this.httpOptions);
  }
}
