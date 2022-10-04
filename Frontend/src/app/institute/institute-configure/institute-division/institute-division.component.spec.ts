import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstituteDivisionComponent } from './institute-division.component';

describe('InstituteDivisionComponent', () => {
  let component: InstituteDivisionComponent;
  let fixture: ComponentFixture<InstituteDivisionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstituteDivisionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstituteDivisionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
