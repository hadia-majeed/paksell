import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search',
  imports: [FontAwesomeModule,FormsModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {
  faSearch = faSearch;
  keyword: string ='';
  selectedCategory: string = '';
  selectedCityArea: string = '';


  onsearch() {
    console.log('searching for:',
      'keyword:', this.keyword,
      'category:', this.selectedCategory,
      'cityArea:', this.selectedCityArea,
    )};
}

