import { TestBed } from '@angular/core/testing';

import { WebGlService } from './web-gl.service';

describe('WebGlService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WebGlService = TestBed.get(WebGlService);
    expect(service).toBeTruthy();
  });
});
