import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CityArea } from '../models/advertisement.model';
import { environment } from './environment';

@Injectable({
  providedIn: 'root'
})
export class CityAreaService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCityAreas(): Observable<CityArea[]>{
    return this.http.get<CityArea[]>(`${this.apiUrl}/CityArea`);
  }
}
