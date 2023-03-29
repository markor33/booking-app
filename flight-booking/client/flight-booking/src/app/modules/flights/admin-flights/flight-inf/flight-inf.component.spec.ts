import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlightInfComponent } from './flight-inf.component';

describe('FlightInfComponent', () => {
  let component: FlightInfComponent;
  let fixture: ComponentFixture<FlightInfComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlightInfComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FlightInfComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
