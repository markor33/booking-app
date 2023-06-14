import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccommodationDisplayDialogComponent } from './accommodation-display-dialog.component';

describe('AccommodationDisplayDialogComponent', () => {
  let component: AccommodationDisplayDialogComponent;
  let fixture: ComponentFixture<AccommodationDisplayDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccommodationDisplayDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccommodationDisplayDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
