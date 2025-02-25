import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WalletsModule } from './modules/wallets.module';
import { UsersModule } from './modules/users.module';
import { CoinsModule } from './modules/coins.module';
import { TransactionsModule } from './modules/transactions.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    WalletsModule,
    UsersModule,
    CoinsModule,
    TransactionsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
