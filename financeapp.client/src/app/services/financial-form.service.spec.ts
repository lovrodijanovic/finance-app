import { TestBed } from '@angular/core/testing';

import { FinancialFormService } from './financial-form.service';

describe('FinancialFormService', () => {
  let service: FinancialFormService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FinancialFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
