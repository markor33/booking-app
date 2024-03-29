import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map } from 'rxjs';
import { LoginRequest } from '../model/login-request';
import { LoginResponse } from '../model/login-response';
import { JwtHelperService } from '@auth0/angular-jwt';
import { RegisterRequest } from '../model/register-request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  private userClaims: any = null;
  private loginSource = new BehaviorSubject<boolean>(false);
  public loginObserver = this.loginSource.asObservable();

  constructor(private httpClient: HttpClient, private jwtHelper: JwtHelperService) { }

  login(loginRequest: LoginRequest): Observable<boolean> {
    return this.httpClient.post<LoginResponse>('api/auth/login', loginRequest, this.httpOptions).pipe(
      map((res) => {
        localStorage.setItem('token', res.jwt);
        this.userClaims = this.jwtHelper.decodeToken();
        this.loginSource.next(true);
        return true;
      })
    );;
  }

  register(registerRequest: RegisterRequest): Observable<boolean> {
    return this.httpClient.post<any>('api/auth/register', registerRequest, this.httpOptions).pipe(
      map((res) => {
        return true;
      })
    );
  }

  logout(): void {
    localStorage.clear();
    this.loginSource.next(false);
  }

  getUserRole(): string {
    return this.userClaims.role;
  }

  isLogged(): boolean {
    if (!this.jwtHelper.tokenGetter())
      return false;
    return true;
  }

}
