import { NgModule } from '@angular/core';
import { WalletsService } from '../services/wallets.service';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { WalletCoinsComponent } from '../components/wallets/wallet-coins/wallet-coins.component';
import { RouterModule } from '@angular/router';
import { WalletComponent } from '../components/wallets/wallet/wallet.component';
import { WalletsComponent } from '../components/wallets/wallets/wallets.component';


@NgModule({
  exports: [
    WalletsComponent, 
    WalletCoinsComponent, 
    WalletComponent
  ],
  declarations: [
    WalletsComponent, 
    WalletCoinsComponent, 
    WalletComponent
  ],
  imports: [
    BrowserModule, 
    HttpClientModule, 
    RouterModule 
  ],
  providers: [
    WalletsService
  ]
})
export class WalletsModule { }