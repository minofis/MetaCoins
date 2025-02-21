import { Component } from '@angular/core';
import { ICoin } from '../../models/coin';
import { CoinsService } from '../../services/coins.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-coins-list',
  templateUrl: './coins-list.component.html',
  styleUrl: './coins-list.component.scss'
})
export class CoinsListComponent {

  public coins$?: Observable<ICoin[]>

  constructor(private _coinsService: CoinsService) {}

  public ngOnInit(): void
  {
    this.coins$ = this._coinsService.getCoins()
  }
}
