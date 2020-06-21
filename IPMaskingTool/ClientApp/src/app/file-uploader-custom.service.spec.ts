import { TestBed } from '@angular/core/testing';

import { FileUploaderCustom } from './file-uploader-custom.service';

describe('FileUploaderCustomService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FileUploaderCustom = TestBed.get(FileUploaderCustom);
    expect(service).toBeTruthy();
  });
});
