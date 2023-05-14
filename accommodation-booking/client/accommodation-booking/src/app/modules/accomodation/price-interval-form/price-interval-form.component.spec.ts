import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PriceIntervalFormComponent } from './price-interval-form.component';

describe('PriceIntervalFormComponent', () => {
  let component: PriceIntervalFormComponent;
  let fixture: ComponentFixture<PriceIntervalFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PriceIntervalFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PriceIntervalFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
