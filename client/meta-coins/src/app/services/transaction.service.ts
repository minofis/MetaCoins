import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/transactions/"

  public sendCoin(transaction: {senderUsername: string, recipientUsername: string, coinId: string}): Observable<any>
  {
    return this._httpClient.post<any>(this.baseServerUrl + 'transfer-coin', transaction, 
      {
        withCredentials: true,
        responseType: 'text' as 'json'
      }
    )
  }
}
