import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiKey } from '../models/api-key.model';

@Injectable({
  providedIn: 'root'
})
export class ApiKeyService {

  constructor(private httpClient: HttpClient) { }

  get(): Observable<ApiKey> {
    return this.httpClient.get<ApiKey>('api/apiKey');
  }

  create(isPermanent: boolean): Observable<ApiKey> {
    return this.httpClient.post<ApiKey>(`api/apiKey?isPermanent=${isPermanent}`, { });
  }

  update(apiKey: ApiKey, isPermanent: boolean): Observable<any> {
    return this.httpClient.put<any>(`api/apiKey/${apiKey.id}?isPermanent=${isPermanent}`, { });
  }

  refresh(apiKey: ApiKey): Observable<any> {
    return this.httpClient.put<any>(`api/apiKey/${apiKey.id}/refresh`, { });
  }

  delete(apiKey: ApiKey): Observable<any> {
    return this.httpClient.delete<any>(`api/apiKey/${apiKey.id}`);
  }

}
