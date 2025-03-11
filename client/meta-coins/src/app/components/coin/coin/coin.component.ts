import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { CoinService } from '../../../services/coin.service';
import { ICoin } from '../../../models/coin';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-coin',
  templateUrl: './coin.component.html',
  styleUrl: './coin.component.scss'
})
export class CoinComponent {

  coinId!: string
  coin$?: Observable<ICoin>

  constructor(private _coinService: CoinService, private route: ActivatedRoute, private location: Location) {}

  public ngOnInit(): void
  {
    this.coinId = this.route.snapshot.paramMap.get('id') || '';
    this.coin$ = this._coinService.getCoin(this.coinId);
  }

  goBack()
  {
    this.location.back();
  }
}
