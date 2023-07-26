import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FullOfferComponent } from './full-offer.component';

describe('FullOfferComponent', () => {
  let component: FullOfferComponent;
  let fixture: ComponentFixture<FullOfferComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FullOfferComponent]
    });
    fixture = TestBed.createComponent(FullOfferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
