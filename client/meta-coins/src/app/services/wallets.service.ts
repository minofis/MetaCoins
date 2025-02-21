import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IWallet } from '../models/wallet';

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
}
