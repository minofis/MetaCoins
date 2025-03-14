import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeedComponent } from '../components/pages/feed/feed.component';



@NgModule({
  declarations: [
    FeedComponent,
  ],
  exports: [
    FeedComponent
  ],
  imports: [
    CommonModule
  ]
})
export class PagesModule { }
