import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  username?: string | null;
  isAuthenticated = false;

  constructor(private authService: AuthService, private router: Router){}

  ngOnInit(): void
  {
    this.authService.isAuthenticated$.subscribe((authStatus) => {
      this.isAuthenticated = authStatus;
    });

    this.authService.username$.subscribe((username) => {
      this.username = username;
    });
  }

  logout(): void
  {
    this.authService.logout();
    this.username = null;
    this.isAuthenticated = false;
    this.router.navigate(['/register']);
  }
}