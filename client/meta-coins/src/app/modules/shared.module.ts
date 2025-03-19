import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LikeComponent } from '../components/shared/like/like.component';
import { LikeService } from '../services/like.service';



@NgModule({
  exports: [
    LikeComponent
  ],
  declarations: [
    LikeComponent
  ],
  imports: [
    CommonModule
  ],
  providers: [
    LikeService
  ]
})
export class SharedModule { }
