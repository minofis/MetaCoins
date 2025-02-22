import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ICoin } from '../../../models/coin';
import { CoinsService } from '../../../services/coins.service';

@Component({
  selector: 'app-coin',
  templateUrl: './coin.component.html',
  styleUrl: './coin.component.scss'
})
export class CoinComponent {

  coinId!: string
  coin$?: Observable<ICoin>

  constructor(private _coinsService: CoinsService, private route: ActivatedRoute, private location: Location) {}

  public ngOnInit(): void
  {
    this.coinId = this.route.snapshot.paramMap.get('id') || '';
    this.coin$ = this._coinsService.getCoin(this.coinId);
  }

  goBack()
  {
    this.location.back();
  }
}

