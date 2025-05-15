import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeLogPageComponent } from './home-log-page.component';

describe('HomeLogPageComponent', () => {
  let component: HomeLogPageComponent;
  let fixture: ComponentFixture<HomeLogPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeLogPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeLogPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
