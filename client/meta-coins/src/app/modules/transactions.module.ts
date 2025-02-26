import { NgModule } from '@angular/core';
import { SendCoinComponent } from '../components/transactions/send-coin/send-coin.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { TransactionsService } from '../services/transactions.service';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    SendCoinComponent,
  ],
  exports: [
    SendCoinComponent,
  ],
  imports: [
    BrowserModule, 
    HttpClientModule,
    RouterModule,
    ReactiveFormsModule
  ],
  providers: [
    TransactionsService
  ]
})
export class TransactionsModule { }
