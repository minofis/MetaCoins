import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  errorMessage!: string

  public loginForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(private _authService: AuthService, private router: Router) {}

  onSubmit(): void
  {
    const credentials =
    {
      username: this.loginForm.value.username ?? '',
      password: this.loginForm.value.password ?? ''
    }
    this._authService.login(credentials).subscribe({
      next: (response) => {
        console.log(response)
        this.router.navigate([`/${this.loginForm.value.username}/profile/`])
      },
      error: (error: HttpErrorResponse) => {
        let errorMessage = 'An error occurred.';
        if (error.message) {
          errorMessage = error.message;
        }
        this.errorMessage = errorMessage
      }
    });
  }
}