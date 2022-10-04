import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstituteBoardComponent } from './institute-board.component';

describe('InstituteBoardComponent', () => {
  let component: InstituteBoardComponent;
  let fixture: ComponentFixture<InstituteBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstituteBoardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstituteBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
