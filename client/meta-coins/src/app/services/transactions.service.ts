import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {

  constructor(private _httpClient: HttpClient){}

/*
  baseServerUrl: string = "http://localhost:5244/meta-coins/transactions/"

  public getTransactions () : Observable<ITransaction[]>
  {
    return this._httpClient.get<ITransaction[]>(this.baseServerUrl)
  }

  public sendCoin(transaction: {}): Observable<string>
  {
    return this._httpClient.post<string>(this.baseServerUrl + 'transfer-coin', transaction, 
      {
        withCredentials: true,
        responseType: 'text' as 'json'
      }
    )
  }
*/
}