import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-signup',
  imports: [CommonModule,FormsModule ],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {

    signupData={
      name:'',
      email:'',
      password:'',
      loginId:'',
      phoneNumber:'',
      city:'',
      userImage:'',
      birthDate:'',
      securityQuestion:'',
      securityAnswer:''
    }
  
    constructor(private authService: AuthService, ) {}
    
      signUp(){
        this.authService.SignUp(this.signupData).subscribe(
          (res) => {
            console.log('signUp Response:', res);
             this.authService.saveToken(res.token);
            alert('signUp successful!');
            console.log('signUp successful!', res);
            window.location.reload();
            // this.router.navigate(['/dashboard']);
            // private router: Router
          
          },
          (err) => {
            alert('signUp failed!');
          }
        )
      }
  }
  

