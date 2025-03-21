import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeedComponent } from '../components/pages/feed/feed.component';
import { RouterModule } from '@angular/router';
import { ProfileComponent } from '../components/pages/profile/profile.component';
import { NotFoundComponent } from '../components/pages/not-found/not-found.component';
import { CoinModule } from './coin.module';



@NgModule({
  declarations: [
    FeedComponent,
    ProfileComponent,
    NotFoundComponent
  ],
  exports: [
    FeedComponent,
    ProfileComponent,
    NotFoundComponent
  ],
  imports: [
    CommonModule,
    CoinModule,
    RouterModule,
  ]
})
export class PagesModule { }
