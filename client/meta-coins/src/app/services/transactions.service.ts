import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ITransaction } from '../models/transaction';
import { TransactionCreateRequest } from '../models/transaction-create-request';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {

  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/transactions/"

  public getTransactions () : Observable<ITransaction[]>
  {
    return this._httpClient.get<ITransaction[]>(this.baseServerUrl)
  }

  public sendCoin(transactionData: TransactionCreateRequest): Observable<string>
  {
    return this._httpClient.post<string>(this.baseServerUrl + 'transfer-coin', transactionData, 
      {
        withCredentials: true,
        responseType: 'text' as 'json'
      }
    )
  }
}