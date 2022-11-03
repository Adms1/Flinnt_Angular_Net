import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportStudentUploadComponent } from './import-student-upload.component';

describe('ImportStudentUploadComponent', () => {
  let component: ImportStudentUploadComponent;
  let fixture: ComponentFixture<ImportStudentUploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportStudentUploadComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportStudentUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
