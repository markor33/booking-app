import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccomodationInfoComponent } from './accomodation-info.component';

describe('AccomodationInfoComponent', () => {
  let component: AccomodationInfoComponent;
  let fixture: ComponentFixture<AccomodationInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccomodationInfoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccomodationInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
