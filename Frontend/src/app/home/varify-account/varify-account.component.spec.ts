import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VarifyAccountComponent } from './varify-account.component';

describe('VarifyAccountComponent', () => {
  let component: VarifyAccountComponent;
  let fixture: ComponentFixture<VarifyAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VarifyAccountComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VarifyAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
