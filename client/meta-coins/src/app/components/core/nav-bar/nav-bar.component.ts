import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  username?: string | null

  constructor(private authService: AuthService){}

  ngOnInit(): void
  {
    this.authService.username$.subscribe((username) => {
      this.username = username
    });
  }
}
