import { Component } from '@angular/core';
import { ICoin } from '../../../models/coin';
import { CoinsService } from '../../../services/coins.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-coins',
  templateUrl: './coins.component.html',
  styleUrl: './coins.component.scss'
})
export class CoinsComponent {

  public coins$?: Observable<ICoin[]>

  constructor(private _coinsService: CoinsService) {}

  public ngOnInit(): void
  {
    this.coins$ = this._coinsService.getCoins()
  }
}
