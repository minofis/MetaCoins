import { NgModule } from '@angular/core';
import { CoinService } from '../services/coin.service';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { CoinComponent } from '../components/coin/coin/coin.component';
import { OwnerRecordsComponent } from '../components/coin/owner-records/owner-records.component';
import { CreateCoinComponent } from '../components/coin/create-coin/create-coin.component';
import { SharedModule } from './shared.module';


@NgModule({
  declarations: [
    CoinComponent,
    OwnerRecordsComponent,
    CreateCoinComponent,
  ],
  exports: [
    CoinComponent,
    OwnerRecordsComponent,
    CreateCoinComponent,
  ],
  imports: [
    BrowserModule, 
    HttpClientModule,
    RouterModule,
    SharedModule
  ],
  providers: [
    CoinService
  ]
})
export class CoinModule { }
