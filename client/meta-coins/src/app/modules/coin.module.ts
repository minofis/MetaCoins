import { NgModule } from '@angular/core';
import { CoinService } from '../services/coin.service';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { CoinComponent } from '../components/coin/coin/coin.component';
import { OwnerRecordsComponent } from '../components/coin/owner-records/owner-records.component';
import { CreateCoinComponent } from '../components/coin/create-coin/create-coin.component';
import { SharedModule } from './shared.module';
import { SortCoinsComponent } from '../components/coin/sort-coins/sort-coins.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    CoinComponent,
    OwnerRecordsComponent,
    CreateCoinComponent,
    SortCoinsComponent,
  ],
  exports: [
    CoinComponent,
    OwnerRecordsComponent,
    CreateCoinComponent,
    SortCoinsComponent,
  ],
  imports: [
    BrowserModule, 
    HttpClientModule,
    RouterModule,
    SharedModule,
    ReactiveFormsModule,
  ],
  providers: [
    CoinService
  ]
})
export class CoinModule { }
