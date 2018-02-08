import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtcListComponent } from './atc-list.component';

describe('AtcListComponent', () => {
  let component: AtcListComponent;
  let fixture: ComponentFixture<AtcListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtcListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtcListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
