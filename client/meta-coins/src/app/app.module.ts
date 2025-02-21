import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WalletsModule } from './modules/wallets.module';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { UsersModule } from './modules/users.module';
import { CoinsListComponent } from './components/coins-list/coins-list.component';
import { CoinsModule } from './modules/coins.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    WalletsModule,
    UsersModule,
    CoinsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
