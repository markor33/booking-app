import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlightBookingDialogComponent } from './flight-booking-dialog.component';

describe('FlightBookingDialogComponent', () => {
  let component: FlightBookingDialogComponent;
  let fixture: ComponentFixture<FlightBookingDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlightBookingDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FlightBookingDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
