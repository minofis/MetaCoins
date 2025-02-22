import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICoin } from '../models/coin';
import { ICoinOwnerRecord } from '../models/coin-owner-record';

@Injectable({
  providedIn: 'root'
})
export class CoinsService {

  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/coins/"

  public getCoins () : Observable<ICoin[]>
  {
    return this._httpClient.get<ICoin[]>(this.baseServerUrl, {
      withCredentials: true,
    })
  }
  public getCoin (coinId: string) : Observable<ICoin>
  {
    return this._httpClient.get<ICoin>(this.baseServerUrl + coinId, {
      withCredentials: true,
    })
  }
  public getCoinOwnerRecords (coinId: string) : Observable<ICoinOwnerRecord[]>
  {
    return this._httpClient.get<ICoinOwnerRecord[]>(this.baseServerUrl + coinId + "/ownership-records", {
      withCredentials: true,
    });
  }
}
