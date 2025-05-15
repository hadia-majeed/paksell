import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { PostAdFormComponent } from '../post-ad-form/post-ad-form.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-logged-page',
  standalone: true,
  imports: [NavbarComponent, RouterModule, CommonModule, PostAdFormComponent],
  templateUrl: './logged-page.component.html',
  styleUrl: './logged-page.component.css'
})
export class LoggedPageComponent implements OnInit {
  userImage: string = 'assets/kirby.jpg'; // Default image
  showPostForm: boolean = false;
  user: any = null;
 
  constructor(public authService: AuthService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['showPostForm'] === 'true') {
        this.showPostForm = true;
      }
    });
    // Initial check for current user data
    const currentUser = this.authService.getUser();

    if (currentUser && (currentUser.UserImage || currentUser.userImage)) {
      this.userImage = currentUser.UserImage || currentUser.userImage;
    } 
  
    // Subscribe to user updates
    this.authService.user$.subscribe(user => {
      console.log('User data updated:', user);
      if (user) {
        // Check both possible property names (UserImage and userImage)
        if (user.UserImage) {
          this.userImage = user.UserImage;
        } else if (user.userImage) {
          this.userImage = user.userImage;
        } else {
          this.userImage = 'assets/kirby.jpg'; // Default image if no user image
        }
      } else {
        this.userImage = 'assets/kirby.jpg'; // Default image if no user
      }
    });
  }
  
  logout() {
    this.authService.logout();
    this.showPostForm = false; 
    this.router.navigate(['/']);
  }
  togglePostForm() {
    this.showPostForm = !this.showPostForm;
  }
}
