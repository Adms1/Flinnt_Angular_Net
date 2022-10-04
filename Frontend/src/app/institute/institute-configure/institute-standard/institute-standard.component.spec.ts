import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstituteStandardComponent } from './institute-standard.component';

describe('InstituteStandardComponent', () => {
  let component: InstituteStandardComponent;
  let fixture: ComponentFixture<InstituteStandardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstituteStandardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstituteStandardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
