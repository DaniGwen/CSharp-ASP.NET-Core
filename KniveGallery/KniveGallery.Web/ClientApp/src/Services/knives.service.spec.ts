import { TestBed } from '@angular/core/testing';

import { KnivesService } from './knives.service';

describe('knivesService', () => {
  let service: KnivesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KnivesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
