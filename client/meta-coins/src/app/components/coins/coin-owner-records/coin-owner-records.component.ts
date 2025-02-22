import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { WalletsService } from '../../../services/wallets.service';
import { ICoin } from '../../../models/coin';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ICoinOwnerRecord } from '../../../models/coin-owner-record';
import { CoinsService } from '../../../services/coins.service';

@Component({
  selector: 'app-coin-owner-records',
  templateUrl: './coin-owner-records.component.html',
  styleUrl: './coin-owner-records.component.scss'
})
export class CoinOwnerRecordsComponent {

  coinId!: string;
  public ownerRecords$?: Observable<ICoinOwnerRecord[]>;

  constructor(private _coinsService: CoinsService, private route: ActivatedRoute, private location: Location) {}

  public ngOnInit(): void
  {
    this.coinId = this.route.snapshot.paramMap.get('id') || '';
    this.ownerRecords$ = this._coinsService.getCoinOwnerRecords(this.coinId);
  }

  goBack()
  {
    this.location.back();
  }
}
