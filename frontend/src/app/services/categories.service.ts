import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../models/advertisement.model';
import { environment } from './environment';
@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  Getcategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.apiUrl}/AdvertisementCategories`);
  }

  GetbyCategory(categoryId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Advertisement/GetByCategory?categoryId=${categoryId}`);
  }

  GetAdvertisementbyId(AdvertisementId: number): Observable<any> {
    return this.http.get<any[]>(`${this.apiUrl}/Advertisement/${AdvertisementId}`);
  }
  

}