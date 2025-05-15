import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, tap } from 'rxjs';
import { environment } from './environment';
import { jwtDecode } from 'jwt-decode'; // Make sure to install: npm install jwt-decode

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginUrl = environment.loginUrl;
  private signupUrl = environment.signupUrl;
  private tokenKey = 'auth_token';
  private userKey = 'auth_user';

  // Directly initialize userSubject to prevent undefined errors
  private userSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  user$: Observable<any> = this.userSubject.asObservable();

  constructor(private http: HttpClient) {
    this.initializeUserFromStorage();
  }

  private initializeUserFromStorage(): void {
    const storedUser = this.getStoredUser();
    if (storedUser) {
      this.userSubject.next(storedUser); // Update behavior subject with stored user
      console.log("User initialized from LocalStorage:", storedUser);
    }
  }
  
  SignUp(signupData: any): Observable<any> {
    return this.http.post(`${this.signupUrl}`, signupData);
  }

  LogIn(loginData: any): Observable<any> {
    return this.http.post(`${this.loginUrl}`, loginData).pipe(
      tap((response: any) => {
        if (response && response.token) {
          // Parse JWT to get user info
          try {
            const decodedToken = jwtDecode(response.token);
            console.log('Decoded token:', decodedToken);
            
            // Ensure user object has consistent format
            const userId = this.extractUserIdFromToken(decodedToken);
            if (userId && (!response.user || !response.user.id)) {
              if (!response.user) response.user = {};
              response.user.id = userId;
            }
          } catch (error) {
            console.error('Error decoding JWT token:', error);
          }
        }
      })
    );
  }
  
  private extractUserIdFromToken(decodedToken: any): number | null {
    // Try various common claim formats for user ID
    if (decodedToken.sub) return parseInt(decodedToken.sub);
    if (decodedToken.nameid) return parseInt(decodedToken.nameid);
    if (decodedToken.UserId) return parseInt(decodedToken.UserId);
    if (decodedToken.userId) return parseInt(decodedToken.userId);
    if (decodedToken.user_id) return parseInt(decodedToken.user_id);
    
    // Look for any claim that might contain user ID
    for (const key in decodedToken) {
      if (key.toLowerCase().includes('id') && key.toLowerCase().includes('user')) {
        const value = decodedToken[key];
        if (value && !isNaN(parseInt(value.toString()))) {
          return parseInt(value.toString());
        }
      }
    }
    
    return null;
  }

  saveToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  saveUser(user: any): void {
    // Ensure we have consistent property names
    if (user) {
      // If the backend sends UserImage but we want to use userImage consistently
      if (user.UserImage && !user.userImage) {
        user.userImage = user.UserImage;
      }
      
      // Ensure user has an id property
      if (!user.id) {
        const token = this.getToken();
        if (token) {
          try {
            const decodedToken = jwtDecode(token);
            const userId = this.extractUserIdFromToken(decodedToken);
            if (userId) {
              user.id = userId;
            }
          } catch (error) {
            console.error('Error extracting user ID from token:', error);
          }
        }
      }
    }
    
    localStorage.setItem(this.userKey, JSON.stringify(user));
    console.log("Saving user to localStorage:", user);
    this.userSubject.next(user); // Notify all components about user update
  }

  getUser(): any {
    const currentUser = this.userSubject.value;
    if (!currentUser) {
      // If no user in BehaviorSubject, try to get from localStorage
      return this.getStoredUser();
    }
    return currentUser;
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userKey);
    this.userSubject.next(null); // Notify components that user is logged out
  }

  private getStoredUser(): any {
    const userData = localStorage.getItem(this.userKey);
    try {
      return userData ? JSON.parse(userData) : null;
    } catch (e) {
      console.error("Error parsing user data from localStorage:", e);
      return null;
    }
  }
}