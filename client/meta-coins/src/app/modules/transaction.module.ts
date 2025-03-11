import { NgModule } from '@angular/core';
import { SendCoinComponent } from '../components/transaction/send-coin/send-coin.component';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { TransactionService } from '../services/transaction.service';



@NgModule({
  declarations: [
    SendCoinComponent
  ],
  exports: [
    SendCoinComponent
  ],
  imports: [
    BrowserModule, 
    HttpClientModule,
    RouterModule,
    ReactiveFormsModule
  ],
  providers: [
    TransactionService
  ]
})
export class TransactionModule { }
