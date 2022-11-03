import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportStudentUploadingComponent } from './import-student-uploading.component';

describe('ImportStudentUploadingComponent', () => {
  let component: ImportStudentUploadingComponent;
  let fixture: ComponentFixture<ImportStudentUploadingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportStudentUploadingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportStudentUploadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
