import { NgModule } from '@angular/core';
import { UserLoginComponent } from '../components/user-login/user-login.component';
import { UsersService } from '../services/users.service';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  exports: [
    UserLoginComponent
  ],
  declarations: [
    UserLoginComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, ReactiveFormsModule
  ],
  providers: [
    UsersService
  ]
})
export class UsersModule { }
