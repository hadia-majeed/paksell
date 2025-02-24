import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {AuthService} from '../../services/auth.service';
import { SignupComponent } from '../signup/signup.component';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,CommonModule,SignupComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  loginData = {
    loginId:'',
    password:''
  }
  constructor(private authService: AuthService, ) {}

  login(){
    this.authService.LogIn(this.loginData).subscribe(
      (res) => {
        console.log('Login Response:', res);
         this.authService.saveToken(res.token);
        alert('Login successful!');
        console.log('Login successful!', res);
        window.location.reload();
        // this.router.navigate(['/dashboard']);
        // private router: Router
      
      },
      (err) => {
        alert('Login failed!');
      }
    )
  }

}

