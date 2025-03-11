import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/users/"

  login(credentials: { username: string, password: string }): Observable<any>
  {
    return this._httpClient.post<any>(this.baseServerUrl + "login", credentials, 
      { 
        withCredentials: true,
        responseType: 'text' as 'json'
      }
    );
  }
  
  register(user: { username: string, email: string, password: string }): Observable<any>
  {
    return this._httpClient.post<any>(this.baseServerUrl + "register", user, 
      { 
        withCredentials: true,
      }
    );
  }
}