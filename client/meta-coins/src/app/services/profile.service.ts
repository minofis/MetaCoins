import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProfile } from '../models/profile';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private _httpClient: HttpClient){}

  baseServerUrl: string = "http://localhost:5244/meta-coins/profiles/"

  getProfile(username: string) : Observable<IProfile>
  {
    return this._httpClient.get<IProfile>(this.baseServerUrl + 'by-username/' + username, {withCredentials: true,});
  }
}
