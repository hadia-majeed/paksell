import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CarouselComponent } from './components/carousel/carousel.component';
import { SearchComponent } from './components/search/search.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { LoginComponent } from "./components/login/login.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavbarComponent,
    CarouselComponent,
    SearchComponent,
    LoginComponent,
    CategoriesComponent
  ],
  template: `
    <app-login></app-login>
    <app-navbar></app-navbar>
    <app-carousel></app-carousel>
    <app-search></app-search>
    <app-categories></app-categories>
    <router-outlet></router-outlet>
  `,
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'paksell-frontend';
}