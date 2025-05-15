import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
    signupData = {
        name: '',
        email: '',
        password: '',
        loginId: '',
        phoneNumber: '',
        birthDate: '',
        securityQuestion: '',
        securityAnswer: '',
        city: '',
        userImage: '' // This will store the base64 image data
    };

    previewImage: string | ArrayBuffer | null = null; // For image preview

    constructor(private authService: AuthService) {}

    // Handle image selection
    onFileSelected(event: any) {
      const file = event.target.files?.[0];
      if (!file) return;
      
      // Create elements for resizing
      const reader = new FileReader();
      const img = new Image();
      
      reader.onload = (e) => {
        img.onload = () => {
          // Create canvas for resizing
          const canvas = document.createElement('canvas');
          
          // Set max dimensions to 800x800 pixels
          let width = img.width;
          let height = img.height;
          
          if (width > height && width > 800) {
            height = height * (800 / width);
            width = 800;
          } else if (height > 800) {
            width = width * (800 / height);
            height = 800;
          }
          
          canvas.width = width;
          canvas.height = height;
          
          // Draw and get resized image
          const ctx = canvas.getContext('2d');
          if (ctx) {
            ctx.drawImage(img, 0, 0, width, height);
          }
          // Lower quality (0.5) for smaller file size
          const resizedImage = canvas.toDataURL('image/jpeg', 0.5);
          
          // Set preview and save data
          this.previewImage = resizedImage;
          this.signupData.userImage = resizedImage;
        };
        
        img.src = (e.target?.result as string) || '';
      };
      
      reader.readAsDataURL(file);
    }
    
    signUp() {
      debugger;
      this.authService.SignUp(this.signupData).subscribe(
          (res) => {
              console.log('signUp Response:', res);
              this.authService.saveToken(res.token);
              alert('SignUp successful!');
              window.location.reload();
          },
          (err) => {
              console.error('SignUp failed:', err);
              if (err.error && err.error.errors) {
                  console.error('Validation Errors:', err.error.errors);
                  alert(`Validation failed! Check console for details.`);
              } else {
                  alert('SignUp failed! Please try again.');
              }
          }
      );
  }
  
  
}
