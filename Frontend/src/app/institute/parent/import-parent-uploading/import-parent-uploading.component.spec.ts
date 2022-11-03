import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportParentUploadingComponent } from './import-parent-uploading.component';

describe('ImportParentUploadingComponent', () => {
  let component: ImportParentUploadingComponent;
  let fixture: ComponentFixture<ImportParentUploadingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportParentUploadingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportParentUploadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
