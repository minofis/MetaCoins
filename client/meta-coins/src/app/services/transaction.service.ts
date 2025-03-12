import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/transactions/"

  public sendCoin(transaction: {senderUsername: string, recipientUsername: string, coinId: string}): Observable<any>
  {
    return this._httpClient.post<any>(this.baseServerUrl + 'transfer-coin', transaction, {withCredentials: true})
    .pipe(
        catchError((error: HttpErrorResponse) => {
          console.error("Coin Transfer Error:", error.error?.message)
          return throwError(() => new Error(error.error?.message) || 'Something went wrong')
        })
      )
  }
}
