import { Injectable } from '@angular/core';
import { IUser } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest } from '../models/login-request';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/users/"

  public getUsers(): Observable<IUser[]>
  {
    return this._httpClient.get<IUser[]>(this.baseServerUrl)
  }

  public onLogin(loginRequest: LoginRequest): Observable<string>
  {
    return this._httpClient.post<string>(this.baseServerUrl + "login", loginRequest, 
    { 
      withCredentials: true,
      responseType: 'text' as 'json'
    });
  }
}
