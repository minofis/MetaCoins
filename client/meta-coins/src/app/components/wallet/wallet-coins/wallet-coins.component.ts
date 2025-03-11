import { Component } from '@angular/core';
import { WalletService } from '../../../services/wallet.service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Location } from '@angular/common';
import { ICoin } from '../../../models/coin';

@Component({
  selector: 'app-wallet-coins',
  templateUrl: './wallet-coins.component.html',
  styleUrl: './wallet-coins.component.scss'
})
export class WalletCoinsComponent {

  username!: string;
  public coins$?: Observable<ICoin[]>;

  constructor(private _walletService: WalletService, private route: ActivatedRoute, private location: Location) {}

  public ngOnInit(): void
  {
    this.username = this.route.snapshot.paramMap.get('username') || '';
    this.coins$ = this._walletService.getWalletCoins(this.username);
  }

  goBack()
  {
    this.location.back();
  }
}
