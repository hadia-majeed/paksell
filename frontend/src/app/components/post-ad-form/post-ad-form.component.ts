import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoriesService } from '../../services/categories.service';
import { AdvertisementService } from '../../services/advertisement.service';
import { Advertisement } from '../../models/advertisement.model';
import { AuthService } from '../../services/auth.service';  
import { privateDecrypt } from 'crypto';

@Component({
  selector: 'app-post-ad-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  providers: [CategoriesService],
  templateUrl: './post-ad-form.component.html',
  styleUrl: './post-ad-form.component.css'
})
export class PostAdFormComponent implements OnChanges, OnInit {
  @Input() selectedAd: any;
  selectedAdvertisement: any;
  categories: any[] = [];
  isSubmitting = false;
  submitSuccess = false;
  submitError = '';

  adForm = {
    title: '',
    price: '',
    description: '',
    category: '',
    startsOn: '',
    endsOn: '',
    imageUrl: '',
    cityArea: '',
    features: ''
  };

  constructor(
    private categoriesService: CategoriesService,
    private advertisementService: AdvertisementService,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  ngOnChanges() {
    if (this.selectedAd) {
      this.fetchAdDetails(this.selectedAd);
    }
  }

  loadCategories(): void {
    this.categoriesService.Getcategories().subscribe(
      (categories) => {
        console.log('Loaded categories:', categories);
        this.categories = categories;
      },
      (error) => {
        console.error('Error loading categories:', error);
      }
    );
  }
  fetchAdDetails(ad: any) {
    this.categoriesService.getMappedAdvertisementById(ad.id).subscribe(
      (mappedAd) => {
        this.selectedAdvertisement = mappedAd;
        this.adForm.title = mappedAd.title;
        this.adForm.price = mappedAd.price;
        this.adForm.description = mappedAd.description;
        this.adForm.category = mappedAd.categoryId; 
        this.adForm.cityArea = mappedAd.cityArea;
        this.adForm.features = mappedAd.features;
        console.log('Fetched advertisement:', this.selectedAdvertisement);
        console.log('Ad Form:', this.adForm);
      },
      (error) => {
        console.error('Error fetching advertisement:', error);
      }
    );
  }

  onSubmit() {
    if (!this.validateForm()) {
      return;
    }

    this.isSubmitting = true;
    this.submitError = '';

    const currentUser = this.authService.getUser();
    const selectedCategory = this.categories.find(category => category.id === parseInt(this.adForm.category, 10));


    const advertisementData: Advertisement = {
      id: 0,
      name: this.adForm.title,
      price: parseFloat(this.adForm.price),
      description: this.adForm.description,
      categoryId: parseInt(this.adForm.category, 10),
      category: selectedCategory ? selectedCategory.name : '',
      imageUrl: this.adForm.imageUrl,
      cityArea: this.adForm.cityArea,
      startsOn: new Date().toISOString(),
      endsOn: new Date(new Date().setMonth(new Date().getMonth() + 1)).toISOString(),
      AdvertisementImages: [],
      AdvertisementFeatures: [],
      userId: currentUser.id ,
      postedBy: currentUser.id 
    };

    console.log('Form submitted:', advertisementData);

    this.advertisementService.Postadvertisement(advertisementData).subscribe(
      (response) => {
        console.log('Advertisement posted successfully:', response);
        this.isSubmitting = false;
        this.submitSuccess = true;

        this.resetForm();
        setTimeout(() => {
          this.router.navigate(['/categories']);
        }, 2000);
      },
      (error) => {
        console.error('Error posting advertisement:', error);
        this.isSubmitting = false;
        this.submitError = 'Failed to post advertisement. Please try again.';
      }
    );
  }

  validateForm(): boolean {
    if (!this.adForm.title || !this.adForm.price || !this.adForm.description ||
        !this.adForm.category || !this.adForm.imageUrl || !this.adForm.cityArea) {
      this.submitError = 'Please fill in all required fields';
      return false;
    }

    if (isNaN(parseFloat(this.adForm.price))) {
      this.submitError = 'Price must be a valid number';
      return false;
    }

    return true;
  }

  resetForm() {
    this.adForm = {
      title: '',
      price: '',
      description: '',
      category: '',
      startsOn: '',
      endsOn: '',
      imageUrl: '',
      cityArea: '',
      features: ''
    };
  }
}
