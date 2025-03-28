import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICoin } from '../models/coin';

@Injectable({
  providedIn: 'root'
})
export class LikeService {

  baseServerUrl: string = "http://localhost:5244/meta-coins/likes/"
  constructor(private _httpClient: HttpClient) {}

  getUserLikedCoins(username: string): Observable<ICoin[]>
  {
    return this._httpClient.get<ICoin[]>(this.baseServerUrl + 'by-username/' + username, {withCredentials: true})
  }

  likeCoin(coinId: string): Observable<void>
  {
    return this._httpClient.post<void>(this.baseServerUrl + 'like-coin/' + coinId, {}, {withCredentials: true})
  }

  unlikeCoin(coinId: string): Observable<void>
  {
    return this._httpClient.delete<void>(this.baseServerUrl + 'unlike-coin/' + coinId, {withCredentials: true})
  }

  isCoinLiked(coinId: string): Observable<boolean>
  {
    return this._httpClient.get<boolean>(this.baseServerUrl + 'is-liked/' + coinId, {withCredentials: true})
  }
}
