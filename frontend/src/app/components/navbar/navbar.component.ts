import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { LoginComponent } from '../login/login.component';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [LoginComponent, CommonModule, FormsModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  showLoginModal: boolean = false; 
  constructor(private authService: AuthService, private router: Router) {}
  handlePostAdClick() {
    if (this.authService.getUser()) {
      // User is logged in, show the post ad form
      this.router.navigate(['/logged-page'], { queryParams: { showPostForm: 'true' } }); // Navigate to logged page
    } else {
      // User is not logged in, show login modal
      this.showLoginModal = true;
    }
  }
  
  closeLoginModal() {
    this.showLoginModal = false;
  }
}