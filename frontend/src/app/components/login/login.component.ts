import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {AuthService} from '../../services/auth.service';
import { SignupComponent } from '../signup/signup.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,CommonModule, SignupComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  @Input() isModal: boolean = false;
  @Output() loginSuccess = new EventEmitter<void>();
  
  loginData = {
    loginId:'',
    password:''
  }
  constructor(private authService: AuthService,  private router: Router ) {}

  login(){
    debugger;
    this.authService.LogIn(this.loginData).subscribe(
      (res) => {
        console.log('Login Response:', res);
         this.authService.saveToken(res.token);
         this.authService.saveUser(res.user);
        console.log('Login successful!', res);
         // Emit success event if used as modal
        if (this.isModal) {
          this.loginSuccess.emit();
        }
        this.router.navigate(['/home-log-page']);
       
      
      },
      (err) => {
        alert('Login failed!');
      }
    )
  }

}

