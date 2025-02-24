import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/advertisement.model';
import { environment } from './environment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginUrl = environment.loginUrl;
  private signupUrl = environment.signupUrl;


  constructor(private http: HttpClient) {}

  SignUp(signupData: any): Observable<any> {
    return this.http.post(`${this.signupUrl}`, signupData);
  }

  LogIn(loginData: any): Observable<any> {
    return this.http.post(`${this.loginUrl}`, loginData);
  }

  saveToken(token: string): void {
    localStorage.setItem('token', token)
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  logout(): void {
    localStorage.removeItem('token');
  }

}
