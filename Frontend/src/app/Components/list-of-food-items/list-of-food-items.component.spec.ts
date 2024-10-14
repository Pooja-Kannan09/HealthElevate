import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListOfFoodItemsComponent } from './list-of-food-items.component';

describe('ListOfFoodItemsComponent', () => {
  let component: ListOfFoodItemsComponent;
  let fixture: ComponentFixture<ListOfFoodItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListOfFoodItemsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListOfFoodItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
