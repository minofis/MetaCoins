import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IWallet } from '../models/wallet';
import { HttpClient } from '@angular/common/http';
import { ICoin } from '../models/coin';

@Injectable({
  providedIn: 'root'
})
export class WalletService {

  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/wallets/"

  getWallet(username: string) : Observable<IWallet>
  {
    return this._httpClient.get<IWallet>(this.baseServerUrl + 'by-username/' + username, 
    {
      withCredentials: true,
    });
  }

  getWalletCoins (username: string) : Observable<ICoin[]>
  {
    return this._httpClient.get<ICoin[]>(this.baseServerUrl + 'by-username/' + username + "/coins", 
    {
      withCredentials: true,
    });
  }
}