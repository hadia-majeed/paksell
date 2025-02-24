import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Advertisement } from '../models/advertisement.model';
import { environment } from './environment';
@Injectable({
  providedIn: 'root'
})
export class AdvertisementService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  Getadvertisements(): Observable<Advertisement[]> {
    return this.http.get<Advertisement[]>(`${this.apiUrl}/Advertisement`);
  }


  
}