import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Location } from '@angular/common';
import { IWallet } from '../../../models/wallet';
import { WalletsService } from '../../../services/wallets.service';

@Component({
  selector: 'app-wallets',
  templateUrl: './wallets.component.html',
  styleUrl: './wallets.component.scss'
})
export class WalletsComponent {
  public wallets$?: Observable<IWallet[]>

  constructor(private _walletsService: WalletsService, private location: Location) {}

  public ngOnInit(): void
  {
    this.wallets$ = this._walletsService.getWallets()
  }
}