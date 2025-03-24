import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeedComponent } from '../components/pages/feed/feed.component';
import { RouterModule } from '@angular/router';
import { ProfileComponent } from '../components/pages/profile/profile.component';
import { NotFoundComponent } from '../components/pages/not-found/not-found.component';
import { CoinModule } from './coin.module';
import { UserLikesComponent } from '../components/pages/user-likes/user-likes.component';



@NgModule({
  declarations: [
    FeedComponent,
    ProfileComponent,
    NotFoundComponent,
    UserLikesComponent,
  ],
  exports: [
    FeedComponent,
    ProfileComponent,
    NotFoundComponent,
    UserLikesComponent,
  ],
  imports: [
    CommonModule,
    CoinModule,
    RouterModule,
  ]
})
export class PagesModule { }
