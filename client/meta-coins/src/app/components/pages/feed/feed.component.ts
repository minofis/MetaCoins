import { Component } from '@angular/core';
import { CoinService } from '../../../services/coin.service';
import { Observable } from 'rxjs';
import { ICoin } from '../../../models/coin';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.scss'
})
export class FeedComponent {
  public coins$?: Observable<ICoin[]>;

  constructor(private coinService: CoinService) {}

  public ngOnInit(): void
  {
    this.coins$ = this.coinService.getAllCoins();
  }
}
