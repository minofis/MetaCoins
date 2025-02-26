import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { CoinsService } from '../../../services/coins.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CoinCreateRequest } from '../../../models/coin-create-request';

@Component({
  selector: 'app-coin-create',
  templateUrl: './coin-create.component.html',
  styleUrl: './coin-create.component.scss'
})
export class CoinCreateComponent {

  walletId!: string
  message?: string

  constructor(private _coinsService: CoinsService, private router: Router ,private route: ActivatedRoute, private location: Location) {}

  public createCoin()
  {
    this.walletId = this.route.snapshot.paramMap.get('id') || '';

    const coinData: CoinCreateRequest =
    {
      walletId: this.walletId
    }
    
    this._coinsService.createCoin(coinData).subscribe(
      (token) => {
        this.message = token
        console.log(token)
        this.router.navigate(['/wallets', this.walletId, 'coins'])
      }
    );
  }

  goBack()
  {
    this.location.back();
  }
}
