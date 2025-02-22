import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IWallet } from '../models/wallet';
import { ICoin } from '../models/coin';

@Injectable({
  providedIn: 'root'
})
export class WalletsService {

  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/wallets/"

  public getWallets () : Observable<IWallet[]>
  {
    return this._httpClient.get<IWallet[]>(this.baseServerUrl, { 
      withCredentials: true,
    });
  }

  public getWallet (id: string) : Observable<IWallet>
  {
    return this._httpClient.get<IWallet>(this.baseServerUrl + id, {
      withCredentials: true,
    })
  }

  public getWalletCoins (walletId: string) : Observable<ICoin[]>
  {
    return this._httpClient.get<ICoin[]>(this.baseServerUrl + walletId + "/coins", {
      withCredentials: true,
    });
  }
}
