import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Footer404Component } from './footer404.component';

describe('Footer404Component', () => {
  let component: Footer404Component;
  let fixture: ComponentFixture<Footer404Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ Footer404Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(Footer404Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
