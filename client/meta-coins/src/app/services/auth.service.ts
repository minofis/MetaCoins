import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/users/"

  login(credentials: { username: string, password: string }): Observable<any> 
  {
    return this._httpClient.post<any>(this.baseServerUrl + "login", credentials, { withCredentials: true })
    .pipe(
      catchError((error: HttpErrorResponse) => {
        console.error("Login Error:", error.error?.message);
        return throwError(() => new Error(error.error?.message) || 'Something went wrong');
      })
    );
  }
  
  register(user: { username: string, email: string, password: string }): Observable<any>
  {
    return this._httpClient.post<any>(this.baseServerUrl + "register", user, { withCredentials: true })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          console.error("Register Error:", error.error?.message)
          return throwError(() => new Error(error.error?.message) || 'Something went wrong')
      }));
  }
}