import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';
import { CarouselComponent } from '../carousel/carousel.component';
import { SearchComponent } from '../search/search.component';
import { CategoriesComponent } from '../categories/categories.component';
import { LoginComponent } from "../login/login.component";
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ NavbarComponent, CarouselComponent, SearchComponent, CategoriesComponent,RouterModule,LoginComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
