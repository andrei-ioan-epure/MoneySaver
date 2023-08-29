import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignUpResponseComponent } from './sign-up-response.component';

describe('SignUpResponseComponent', () => {
  let component: SignUpResponseComponent;
  let fixture: ComponentFixture<SignUpResponseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SignUpResponseComponent]
    });
    fixture = TestBed.createComponent(SignUpResponseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
