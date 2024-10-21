import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BinaryCheckComponent } from './binary-check.component';

describe('BinaryCheckComponent', () => {
  let component: BinaryCheckComponent;
  let fixture: ComponentFixture<BinaryCheckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BinaryCheckComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BinaryCheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
