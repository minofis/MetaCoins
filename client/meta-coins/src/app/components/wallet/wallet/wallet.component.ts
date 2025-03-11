import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { Observable } from 'rxjs';
import { IWallet } from '../../../models/wallet';
import { WalletService } from '../../../services/wallet.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrl: './wallet.component.scss'
})
export class WalletComponent {

  wallet$?: Observable<IWallet>
  username!: string

  public ngOnInit(): void
  {
    this.username = this.route.snapshot.paramMap.get('username') || '';
    this.wallet$ = this._walletService.getWallet(this.username)
  }

  constructor(private _walletService: WalletService, private route: ActivatedRoute, private location: Location) {}

  goBack()
  {
    this.location.back();
  }
}
