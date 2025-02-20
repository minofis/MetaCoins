import { NgModule } from '@angular/core';
import { WalletsService } from '../services/wallets.service';
import { WalletsListComponent } from '../components/wallets-list/wallets-list.component';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  exports: [
    WalletsListComponent
  ],
  declarations: [
    WalletsListComponent
  ],
  imports: [
    BrowserModule, HttpClientModule
  ],
  providers: [
    WalletsService
  ]
})
export class WalletsModule { }