import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ICoin } from '../../../models/coin';
import { Observable, tap } from 'rxjs';
import { CoinService } from '../../../services/coin.service';

@Component({
  selector: 'app-sort-coins',
  templateUrl: './sort-coins.component.html',
  styleUrl: './sort-coins.component.scss'
})
export class SortCoinsComponent {

  public coins: ICoin[] = [];
  currentPage = 1;
  pageSize = 10;
  totalCoins = 0;
  hasNextPage = false;
  hasPreviousPage = false;

  public ngOnInit(): void
  {
    this.onSubmit();
  }

  public sortCoinsForm = new FormGroup({
    sortOption: new FormControl<string>('')
  });

  public searchCoinsForm = new FormGroup({
    username: new FormControl<string>(''),
    sortOption: new FormControl<string>('')
  });
  
  constructor(private coinService: CoinService) {}

  onSubmit(): void
  {
    this.currentPage = 1; 
    this.loadCoins();
  }

  public loadCoins()
  {
    const selectedOptions = this.searchCoinsForm.get('sortOption')?.value || 'likes-true';
    const filterUsername = this.searchCoinsForm.get('username')?.value || '';

    const [sortBy, descending] = selectedOptions.split('-');
    const isDescending = descending === 'true';

    let query =
    {
      username: filterUsername,
      sortBy: sortBy,
      descending: isDescending,
      pageNumber: this.currentPage,
      pageSize: this.pageSize,
    }

    this.coinService.getAllCoins(query).subscribe(
      (response) => {
        this.coins = response.items;
        this.currentPage = response.page;
        this.pageSize = response.pageSize;
        this.totalCoins = response.totalItems;
        this.hasNextPage = response.hasNextPage;
        this.hasPreviousPage = response.hasPreviousPage;
      }
    );
  }

  public prevPage()
  {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadCoins();
    }
  }

  public nextPage()
  {
    this.currentPage++;
    this.loadCoins();
  }
}