import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IWallet } from '../../models/wallet';
import { WalletsService } from '../../services/wallets.service';

@Component({
  selector: 'app-wallets-list',
  templateUrl: './wallets-list.component.html',
  styleUrl: './wallets-list.component.scss'
})
export class WalletsListComponent {
  public wallets$?: Observable<IWallet[]>

  constructor(private _walletsService: WalletsService) {}

  public ngOnInit(): void
  {
    this.wallets$ = this._walletsService.getWallets()
  }
}