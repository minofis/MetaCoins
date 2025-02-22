import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { WalletsService } from '../../../services/wallets.service';
import { ICoin } from '../../../models/coin';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-wallet-coins',
  templateUrl: './wallet-coins.component.html',
  styleUrl: './wallet-coins.component.scss'
})
export class WalletCoinsComponent {

  walletId!: string;
  public coins$?: Observable<ICoin[]>;

  constructor(private _walletsService: WalletsService, private route: ActivatedRoute, private location: Location) {}

  public ngOnInit(): void
  {
    this.walletId = this.route.snapshot.paramMap.get('id') || '';
    this.coins$ = this._walletsService.getWalletCoins(this.walletId);
  }

  goBack()
  {
    this.location.back();
  }
}