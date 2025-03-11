import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Observable } from 'rxjs';
import { CoinService } from '../../../services/coin.service';
import { IOwnerRecord } from '../../../models/owner-record';

@Component({
  selector: 'app-owner-records',
  templateUrl: './owner-records.component.html',
  styleUrl: './owner-records.component.scss'
})
export class OwnerRecordsComponent {

  coinId!: string;
  ownerRecords$?: Observable<IOwnerRecord[]>;

  constructor(private _coinService: CoinService, private route: ActivatedRoute, private location: Location) {}

  public ngOnInit(): void
  {
    this.coinId = this.route.snapshot.paramMap.get('id') || '';
    this.ownerRecords$ = this._coinService.getOwnerRecords(this.coinId);
  }

  goBack()
  {
    this.location.back();
  }
}
