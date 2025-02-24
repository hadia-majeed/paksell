import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostAdFormComponent } from './post-ad-form.component';

describe('PostAdFormComponent', () => {
  let component: PostAdFormComponent;
  let fixture: ComponentFixture<PostAdFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PostAdFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PostAdFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
