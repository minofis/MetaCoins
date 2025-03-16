import { Component } from '@angular/core';
import { WalletService } from '../../../services/wallet.service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Location } from '@angular/common';
import { ICoin } from '../../../models/coin';
import { OwnershipService } from '../../../services/ownership.service';

@Component({
  selector: 'app-wallet-coins',
  templateUrl: './wallet-coins.component.html',
  styleUrl: './wallet-coins.component.scss'
})
export class WalletCoinsComponent {

  username?: string;
  isOwner = false;
  public coins$?: Observable<ICoin[]>;

  constructor(private _walletService: WalletService, private route: ActivatedRoute, private location: Location, private ownershipService: OwnershipService) {}

  public ngOnInit(): void
  {
    this.username = this.route.snapshot.paramMap.get('username') || '';
    this.isOwner = this.ownershipService.checkOwnership(this.username);
    this.coins$ = this._walletService.getWalletCoins(this.username);
  }

  goBack()
  {
    this.location.back();
  }
}
