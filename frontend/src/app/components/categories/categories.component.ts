import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CategoriesService } from '../../services/categories.service';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './categories.component.html',
  providers: [CategoriesService]
})
export class CategoriesComponent implements OnInit {
  products: any[] = [];
  selectedCategory: any = null;
  selectedAdvertisement: any = null;
  advertisements: any[] = [];
 

  constructor(private categoriesService: CategoriesService) {}

  ngOnInit(): void {
    this.categoriesService.Getcategories().subscribe(
      (categories) => {
        console.log('Fetched categories:', categories);
        this.products = categories.map(category => ({
          id: category.id,
          name: category.name,
          imageUrl: category.image,
          advertisementCount: category.advertisementCount,
        }));
        console.log('Mapped products:', this.advertisements);
      },
      (error) => {
        console.error('Error fetching categories:', error);
      }
    );
  }

  onCategorySelected(category: any): void {
    this.selectedCategory = category;
    this.advertisements = [];
    this.categoriesService.GetbyCategory(category.id).subscribe(
      (advertisements) => {

        console.log('Fetched advertisements:', advertisements);
        this.advertisements = advertisements;
       
          },
          (error) => {
            console.error('Error fetching advertisements:', error);
          }
        );
        console.log('Mapped advertisements:', this.advertisements);
      }
   
  onAdvertisementSelected(advertisement: any): void{
    this.advertisements = [];
    this.selectedAdvertisement = advertisement;
    this.categoriesService.GetAdvertisementbyId(advertisement.id).subscribe(
      (response) => {
        console.log('Fetched advertisement:', response);
                this.selectedAdvertisement = {
          id: response.id,
          name: response.name,
          price: response.price,
          description: response.description,
          postedBy: response.postedBy.name,
          imageUrl: response.imageUrl,
          startsOn: response.startsOn,
          endsOn: response.endsOn,
          phoneNumber : response.postedBy.phoneNumber ,
          email: response.postedBy.email ,
          cityArea: response.cityArea.name,
          AdvertisementImages: Array.isArray(response.advertisementImages) 
          ? response.advertisementImages.map((image:any) => image?.imagePath ?? '')
          : [],        
            AdvertisementFeatures: response.advertisementFeatures.map((feature: any) => feature.name),
          category: response.category,

          categoryId: response.categoryId,
          userId: response.userId
        };
      },
      (error) => {
        console.error('Error fetching advertisement:', error);
      }
    );    
  }
}