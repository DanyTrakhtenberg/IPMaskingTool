import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Ng2FileLoaderComponent } from './ng2-file-loader.component';

describe('Ng2FileLoaderComponent', () => {
  let component: Ng2FileLoaderComponent;
  let fixture: ComponentFixture<Ng2FileLoaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Ng2FileLoaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Ng2FileLoaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
