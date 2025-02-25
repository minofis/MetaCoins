import { Component } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { LoginRequest } from '../../models/login-request';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrl: './user-login.component.scss'
})
export class UserLoginComponent {
  jwtToken?: string

  public loginForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(private _userService: UsersService, private router: Router) {}

  public onLogin(): void
  {
    const loginRequest: LoginRequest =
    {
      username: this.loginForm.value.username ?? '',
      password: this.loginForm.value.password ?? ''
    }
    this._userService.onLogin(loginRequest).subscribe(
      (token) => {
        this.jwtToken = token
        console.log(token)
        this.router.navigate(['/my-wallets'])
      }
    );
  }
}
