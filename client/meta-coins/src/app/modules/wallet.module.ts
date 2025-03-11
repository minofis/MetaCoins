import { NgModule } from '@angular/core';
import { WalletService } from '../services/wallet.service';
import { WalletComponent } from '../components/wallet/wallet/wallet.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { WalletCoinsComponent } from '../components/wallet/wallet-coins/wallet-coins.component';



@NgModule({
  exports: [
    WalletComponent, 
    WalletCoinsComponent,
  ],
  declarations: [
    WalletComponent, 
    WalletCoinsComponent,
  ],
  imports: [
    BrowserModule, 
    HttpClientModule, 
    RouterModule 
  ],
  providers: [
    WalletService
  ]
})
export class WalletModule { }
