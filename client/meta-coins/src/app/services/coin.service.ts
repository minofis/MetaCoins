import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICoin } from '../models/coin';
import { IOwnerRecord } from '../models/owner-record';
import { IPaginatedResult } from '../models/paginated-result';

@Injectable({
  providedIn: 'root'
})
export class CoinService {

  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/coins/"

  getAllCoins (query: {sortBy: string, descending: boolean, pageNumber: number, pageSize: number, username: string}) : Observable<IPaginatedResult<ICoin>>
  {
    let params = new HttpParams()
      .set('sortBy', query.sortBy)
      .set('descending', query.descending.toString())
      .set('pageNumber', query.pageNumber.toString())
      .set('pageSize', query.pageSize.toString())
      
      if (query.username) {
        params = params.set('username', query.username)
      }
      
    return this._httpClient.get<IPaginatedResult<ICoin>>(this.baseServerUrl, {params, withCredentials: true,});
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