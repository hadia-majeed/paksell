import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../models/advertisement.model';
import { environment } from './environment';
import { map } from 'rxjs/operators';
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
  getMappedAdvertisementById(id: number): Observable<any> {
    return this.GetAdvertisementbyId(id).pipe(
      map((response: any) => ({
        id: response.id,
        name: response.name,
        title: response.title,
        price: response.price,
        description: response.description,
        postedBy: response.postedBy?.name,
        imageUrl: response.imageUrl,
        startsOn: response.startsOn,
        endsOn: response.endsOn,
        phoneNumber: response.postedBy?.phoneNumber,
        email: response.postedBy?.email,
        cityArea: response.cityArea?.name,
        AdvertisementImages: Array.isArray(response.advertisementImages)
          ? response.advertisementImages.map((image: any) => image?.imagePath ?? '')
          : [],
        AdvertisementFeatures: Array.isArray(response.advertisementFeatures)
          ? response.advertisementFeatures.map((feature: any) => feature?.name ?? '')
          : [],
        category: response.category,
        categoryId: response.categoryId,
        userId: response.userId
      }))
    );
  }
  

}