import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ITransaction } from '../models/transaction';

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
}