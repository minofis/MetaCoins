import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICoin } from '../models/coin';
import { IOwnerRecord } from '../models/owner-record';

@Injectable({
  providedIn: 'root'
})
export class CoinService {

  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/coins/"

  getAllCoins () : Observable<ICoin[]>
  {
    return this._httpClient.get<ICoin[]>(this.baseServerUrl, {withCredentials: true,});
  }

  getCoinsSorted (sortBy: string = 'createdAt', descending: boolean = false) : Observable<ICoin[]>
  {
    let params = new HttpParams()
      .set('sortBy', sortBy)
      .set('descending', descending.toString())
      
    return this._httpClient.get<ICoin[]>(this.baseServerUrl + 'sort-by', {params, withCredentials: true,});
  }

  getCoin (coinId: string) : Observable<ICoin>
  {
    return this._httpClient.get<ICoin>(this.baseServerUrl + coinId, {withCredentials: true,});
  }

  getOwnerRecords (coinId: string) : Observable<IOwnerRecord[]>
  {
    return this._httpClient.get<IOwnerRecord[]>(this.baseServerUrl + coinId + "/ownership-records", {
      withCredentials: true,
    });
  }

  createCoin(coin: { username: string }): Observable<any>
  {
    return this._httpClient.post<any>(this.baseServerUrl, coin, 
      { 
        withCredentials: true
      }
    );
  }
}