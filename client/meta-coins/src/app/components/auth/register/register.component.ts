import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {

  errorMessage!: string

  public registerForm = new FormGroup({
    username: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(private _authService: AuthService, private router: Router) {}

    onSubmit(): void
    {
      const user =
      {
        username: this.registerForm.value.username ?? '',
        email: this.registerForm.value.email ?? '',
        password: this.registerForm.value.password ?? ''
      }

      this._authService.register(user).subscribe({
        next: (response) => {
          console.log(response)
          this.router.navigate(['/login'])
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
