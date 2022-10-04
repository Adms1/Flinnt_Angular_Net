import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstituteGroupStructureComponent } from './institute-group-structure.component';

describe('InstituteGroupStructureComponent', () => {
  let component: InstituteGroupStructureComponent;
  let fixture: ComponentFixture<InstituteGroupStructureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstituteGroupStructureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstituteGroupStructureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
