import { Component, OnInit } from '@angular/core';
import { LoggedPageComponent } from '../logged-page/logged-page.component';
import { CarouselComponent } from '../carousel/carousel.component';
import { SearchComponent } from '../search/search.component';
import { CategoriesComponent } from '../categories/categories.component';
import { CommonModule } from '@angular/common';

import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home-log-page',
  standalone: true,
  imports: [LoggedPageComponent, CarouselComponent, CategoriesComponent, SearchComponent, CommonModule],
  templateUrl: './home-log-page.component.html',
  styleUrl: './home-log-page.component.css'
})
export class HomeLogPageComponent {
  constructor(public authService: AuthService) {}
}