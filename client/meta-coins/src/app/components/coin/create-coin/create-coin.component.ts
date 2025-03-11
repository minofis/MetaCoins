import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { CoinService } from '../../../services/coin.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-create-coin',
  templateUrl: './create-coin.component.html',
  styleUrl: './create-coin.component.scss'
})
export class CreateCoinComponent {

  username!: string
  message?: string

  constructor(private _coinService: CoinService, private route: ActivatedRoute, private location: Location) {}

  public createCoin()
  {
    this.username = this.route.snapshot.paramMap.get('username') || '';

    const coin =
    {
      username: this.username
    }
    
    this._coinService.createCoin(coin).subscribe(
      (response) => {
        console.log(response)
        this.location.back();
      }
    );
  }

  goBack()
  {
    this.location.back();
  }
}
