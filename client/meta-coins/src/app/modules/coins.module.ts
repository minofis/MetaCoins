import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { CoinsService } from '../services/coins.service';
import { CoinsComponent } from '../components/coins/coins/coins.component';
import { CoinOwnerRecordsComponent } from '../components/coins/coin-owner-records/coin-owner-records.component';
import { CoinComponent } from '../components/coins/coin/coin.component';
import { RouterModule } from '@angular/router';
import { CoinCreateComponent } from '../components/coins/coin-create/coin-create.component';



@NgModule({
  declarations: [
    CoinsComponent,
    CoinComponent,
    CoinOwnerRecordsComponent,
    CoinCreateComponent,
  ],
  exports: [
    CoinsComponent,
    CoinComponent,
    CoinOwnerRecordsComponent,
    CoinCreateComponent,
  ],
  imports: [
    BrowserModule, 
    HttpClientModule,
    RouterModule
  ],
  providers: [
    CoinsService
  ]
})
export class CoinsModule { }