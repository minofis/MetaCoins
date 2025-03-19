import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LikeService } from '../../../services/like.service';

@Component({
  selector: 'app-like',
  templateUrl: './like.component.html',
  styleUrl: './like.component.scss'
})
export class LikeComponent {

  @Input() likesCount!: number;
  coinId!: string;
  isLiked!: boolean;

  constructor(private route: ActivatedRoute, private likeService: LikeService){}

  public ngOnInit(): void
  {
    this.coinId = this.route.snapshot.paramMap.get('id') || '';
    this.likeService.isCoinLiked(this.coinId).subscribe((response) => {
      this.isLiked = response
    });
  }

  toggleLike(): void
  {
    if (this.isLiked) {
      this.likeService.unlikeCoin(this.coinId).subscribe(() => {
        this.isLiked = false
        this.likesCount--;
      });
    } else{
      this.likeService.likeCoin(this.coinId).subscribe(() => {
        this.isLiked = true
        this.likesCount++;
      })
    };
  }
}
