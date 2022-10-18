import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportParentComponent } from './import-parent.component';

describe('ImportParentComponent', () => {
  let component: ImportParentComponent;
  let fixture: ComponentFixture<ImportParentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportParentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportParentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
