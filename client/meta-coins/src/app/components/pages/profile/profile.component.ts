import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  username?: string | null

  constructor(private authService: AuthService){}

  ngOnInit(): void
  {
    this.authService.username$.subscribe((username) => {
      this.username = username
    });
  }
}
