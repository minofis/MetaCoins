import { Component } from '@angular/core';
import { WalletsService } from '../../../services/wallets.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { IWallet } from '../../../models/wallet';

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrl: './wallet.component.scss'
})
export class WalletComponent {

  walletId!: string
  wallet$?: Observable<IWallet>

  constructor(private _walletsService: WalletsService, private route: ActivatedRoute, private router: Router) {}

  public ngOnInit(): void
  {
    this.walletId = this.route.snapshot.paramMap.get('id') || '';
    this.wallet$ = this._walletsService.getWallet(this.walletId);
  }

  goBack()
  {
    this.router.navigate(['/my-wallets'])
  }
}
