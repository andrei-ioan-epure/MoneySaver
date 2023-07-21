import { TestBed } from '@angular/core/testing';

import { OfertService } from './ofert.service';

describe('OfertService', () => {
  let service: OfertService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfertService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
