import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TransactionsModule } from './modules/transactions.module';
import { AuthModule } from './modules/auth.module';
import { WalletModule } from './modules/wallet.module';
import { CoinModule } from './modules/coin.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    WalletModule,
    AuthModule,
    CoinModule,
    TransactionsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
