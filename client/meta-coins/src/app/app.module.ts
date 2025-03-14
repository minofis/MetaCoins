import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './modules/auth.module';
import { WalletModule } from './modules/wallet.module';
import { CoinModule } from './modules/coin.module';
import { TransactionModule } from './modules/transaction.module';
import { PagesModule } from './modules/pages.module';
import { CoreModule } from './modules/core.module';

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
    TransactionModule,
    PagesModule,
    CoreModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
