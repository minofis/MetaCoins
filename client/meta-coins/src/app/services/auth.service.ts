import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { LoginResponse } from '../models/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseServerUrl: string = "http://localhost:5244/meta-coins/users/"

  private usernameSubject = new BehaviorSubject<string | null>(localStorage.getItem('username'));
  public username$ = this.usernameSubject.asObservable();

  private isAuthenticatedSubject = new BehaviorSubject<boolean>(!!localStorage.getItem('username'));
  public isAuthenticated$ = this.isAuthenticatedSubject.asObservable();
  
  constructor(private _httpClient: HttpClient){}

  login(credentials: { username: string, password: string }): Observable<any> 
  {
    return this._httpClient.post<LoginResponse>(this.baseServerUrl + "login", credentials, { withCredentials: true })
    .pipe(
      tap((response: LoginResponse) => {
        if(response?.username){
          this.setUsername(response.username)
          this.isAuthenticatedSubject.next(true)
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

  logout(): void
  {
    localStorage.removeItem('username');
  }

  setUsername(username: string): void
  {
    localStorage.setItem('username', username);
    this.usernameSubject.next(username);
  }

  getUsername(): string | null
  {
    return this.usernameSubject.value;
  }
}