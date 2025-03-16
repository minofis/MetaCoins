import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeedComponent } from '../components/pages/feed/feed.component';
import { RouterModule } from '@angular/router';
import { ProfileComponent } from '../components/pages/profile/profile.component';
import { NotFoundComponent } from '../components/pages/not-found/not-found.component';



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
    RouterModule
  ]
})
export class PagesModule { }
