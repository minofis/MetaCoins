import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ICoin } from '../../../models/coin';
import { Observable } from 'rxjs';
import { CoinService } from '../../../services/coin.service';

@Component({
  selector: 'app-sort-coins',
  templateUrl: './sort-coins.component.html',
  styleUrl: './sort-coins.component.scss'
})
export class SortCoinsComponent {

  public coins$?: Observable<ICoin[]>;

  public ngOnInit(): void
  {
    this.coins$ = this.coinService.getAllCoins();
  }

  public sortCoinsForm = new FormGroup({
    sortOption: new FormControl<string>('')
  });
  
  constructor(private coinService: CoinService) {}

  onSubmit(): void
  {
    const selectedOptions = this.sortCoinsForm.get('sortOption')?.value || 'likes-true';

    const [sortBy, descending] = selectedOptions.split('-');
    const isDescending = descending === 'true';

    console.log(sortBy, isDescending)

    this.coins$ = this.coinService.getCoinsSorted(sortBy, isDescending);
  }
}