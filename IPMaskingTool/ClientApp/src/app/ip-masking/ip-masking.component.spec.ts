import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IpMaskingComponent } from './ip-masking.component';

describe('IpMaskingComponent', () => {
  let component: IpMaskingComponent;
  let fixture: ComponentFixture<IpMaskingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IpMaskingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IpMaskingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
