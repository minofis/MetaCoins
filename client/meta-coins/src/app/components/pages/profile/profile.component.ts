import { Component } from '@angular/core';
import { ProfileService } from '../../../services/profile.service';
import { IProfile } from '../../../models/profile';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  username!: string
  profile$?: Observable<IProfile>

  constructor(private profileService: ProfileService, private route: ActivatedRoute){}

  ngOnInit(): void
  {
    this.username = this.route.snapshot.paramMap.get('username') || '';

    this.profile$ = this.profileService.getProfile(this.username);
  }
}
