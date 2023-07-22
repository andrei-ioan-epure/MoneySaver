import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfertsComponent } from './oferts.component';

describe('OfertsComponent', () => {
  let component: OfertsComponent;
  let fixture: ComponentFixture<OfertsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OfertsComponent]
    });
    fixture = TestBed.createComponent(OfertsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
