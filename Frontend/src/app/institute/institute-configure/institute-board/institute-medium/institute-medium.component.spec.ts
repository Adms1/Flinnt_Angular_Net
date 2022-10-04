import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstituteMediumComponent } from './institute-medium.component';

describe('InstituteMediumComponent', () => {
  let component: InstituteMediumComponent;
  let fixture: ComponentFixture<InstituteMediumComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstituteMediumComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstituteMediumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
