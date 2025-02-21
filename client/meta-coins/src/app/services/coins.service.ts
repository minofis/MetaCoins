import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICoin } from '../models/coin';

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
  public getCoin (id: string) : Observable<ICoin>
  {
    return this._httpClient.get<ICoin>(this.baseServerUrl + id, {
      withCredentials: true,
    })
  }
}
