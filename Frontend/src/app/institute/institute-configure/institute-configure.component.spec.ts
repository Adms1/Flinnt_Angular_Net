import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstituteConfigureComponent } from './institute-configure.component';

describe('InstituteConfigureComponent', () => {
  let component: InstituteConfigureComponent;
  let fixture: ComponentFixture<InstituteConfigureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstituteConfigureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstituteConfigureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
