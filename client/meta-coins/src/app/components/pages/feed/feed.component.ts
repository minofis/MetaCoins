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

  constructor() {}
}
