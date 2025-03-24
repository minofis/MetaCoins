import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { Observable } from 'rxjs';
import { LikeService } from '../../../services/like.service';
import { ActivatedRoute } from '@angular/router';
import { ICoin } from '../../../models/coin';
import { OwnershipService } from '../../../services/ownership.service';

@Component({
  selector: 'app-user-likes',
  templateUrl: './user-likes.component.html',
  styleUrl: './user-likes.component.scss'
})
export class UserLikesComponent {

  username!: string;
  isOwner = false;
  public coins$?: Observable<ICoin[]>;

  constructor(private _likesService: LikeService, private route: ActivatedRoute, private location: Location, private ownershipService: OwnershipService) {}

  public ngOnInit(): void
  {
    this.username = this.route.snapshot.paramMap.get('username') || '';
    this.isOwner = this.ownershipService.checkOwnership(this.username);

    this.coins$ = this._likesService.getUserLikedCoins(this.username);
  }

  goBack()
  {
    this.location.back();
  }
}
