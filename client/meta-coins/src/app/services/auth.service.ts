import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { LoginResponse } from '../models/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseServerUrl: string = "http://localhost:5244/meta-coins/users/"

  private usernameSubject = new BehaviorSubject<string | null>(null);
  public username$ = this.usernameSubject.asObservable();
  
  constructor(private _httpClient: HttpClient)
  {
    const storedUsername = localStorage.getItem('username');
    if(storedUsername)
    {
      this.usernameSubject.next(storedUsername);
    }
  }

  login(credentials: { username: string, password: string }): Observable<any> 
  {
    return this._httpClient.post<LoginResponse>(this.baseServerUrl + "login", credentials, { withCredentials: true })
    .pipe(
      tap((response: LoginResponse) => {
        if(response?.username){
          this.setUsername(response.username)
        }
      }),
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

  setUsername(username: string): void
  {
    this.usernameSubject.next(username)
    localStorage.setItem('username', username)
  }

  getUsername(): string | null
  {
    return this.usernameSubject.value;
  }
}