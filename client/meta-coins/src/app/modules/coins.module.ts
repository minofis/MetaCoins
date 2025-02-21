import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoinsListComponent } from '../components/coins-list/coins-list.component';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { CoinsService } from '../services/coins.service';



@NgModule({
  declarations: [
    CoinsListComponent
  ],
  exports: [
    CoinsListComponent
  ],
  imports: [
    BrowserModule, HttpClientModule
  ],
  providers: [
    CoinsService
  ]
})
export class CoinsModule { }
