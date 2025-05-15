import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { RouterModule } from '@angular/router';
import { LoggedPageComponent } from '../logged-page/logged-page.component';
import { NavbarComponent } from '../navbar/navbar.component';
@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule,RouterModule,LoggedPageComponent],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {
  user: any;
  userImage: string = 'assets/default-avatar.png';
  
  constructor(private authService: AuthService) {}
  
  ngOnInit() {
    // Get initial user data
    this.user = this.authService.getUser();
    
    // Set initial user image
    if (this.user) {
      this.userImage = this.user.userImage || this.user.UserImage || 'assets/default-avatar.png';
    }
    
    // Subscribe to user updates
    this.authService.user$.subscribe(user => {
      this.user = user;
      if (user) {
        this.userImage = user.userImage || user.UserImage || 'assets/default-avatar.png';
      } else {
        this.userImage = 'assets/default-avatar.png';
      }
    });
  }
}