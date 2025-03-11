import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { LoginComponent } from '../components/auth/login/login.component';
import { RegisterComponent } from '../components/auth/register/register.component';



@NgModule({
  exports: [
    LoginComponent,
    RegisterComponent,
  ],
  declarations: [
    LoginComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule, 
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
  ],
  providers: [
    AuthService
  ]
})
export class AuthModule { }
