import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccomodationCardComponent } from './accomodation-card.component';

describe('AccomodationCardComponent', () => {
  let component: AccomodationCardComponent;
  let fixture: ComponentFixture<AccomodationCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccomodationCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccomodationCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
