import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class OwnershipService {

  username?: string | null

  constructor( private authService: AuthService) { }

  ngOnInit()
  {
    this.authService.username$.subscribe((username) => {
      this.username = username;
    })
  }

  checkOwnership(ownerUsername: string): boolean
  {
    if (ownerUsername == this.username) 
    {
      return true;
    }
    return false;
  }
}
