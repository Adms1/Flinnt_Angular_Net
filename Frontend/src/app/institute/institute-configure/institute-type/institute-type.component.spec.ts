import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstituteTypeComponent } from './institute-type.component';

describe('InstituteTypeComponent', () => {
  let component: InstituteTypeComponent;
  let fixture: ComponentFixture<InstituteTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstituteTypeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstituteTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
