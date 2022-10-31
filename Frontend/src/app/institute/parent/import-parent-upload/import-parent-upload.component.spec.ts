import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportParentUploadComponent } from './import-parent-upload.component';

describe('ImportParentUploadComponent', () => {
  let component: ImportParentUploadComponent;
  let fixture: ComponentFixture<ImportParentUploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportParentUploadComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportParentUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
