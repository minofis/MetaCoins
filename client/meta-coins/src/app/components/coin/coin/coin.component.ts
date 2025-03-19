import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { CoinService } from '../../../services/coin.service';
import { ICoin } from '../../../models/coin';
import { Observable } from 'rxjs';
import { AuthService } from '../../../services/auth.service';
import { OwnershipService } from '../../../services/ownership.service';

@Component({
  selector: 'app-coin',
  templateUrl: './coin.component.html',
  styleUrl: './coin.component.scss'
})
export class CoinComponent {

  coinId!: string
  coin$?: Observable<ICoin>
  username?: string
  isOwner = false;
  isCoinExists = true

  constructor(private _coinService: CoinService, private route: ActivatedRoute, private location: Location, private router: Router, private ownershipService: OwnershipService) {}

  public ngOnInit(): void
  {
    this.coinId = this.route.snapshot.paramMap.get('id') || '';
    this.username = this.route.snapshot.paramMap.get('username') || '';

    this.isOwner = this.ownershipService.checkOwnership(this.username);
    
    this.coin$ = this._coinService.getCoin(this.coinId);

    this.coin$.subscribe(coin => {
      if (coin.ownerUsername != this.username) {
        this.router.navigate(['/not-found'])
      }
    })
  }

  goBack()
  {
    this.location.back();
  }
}
