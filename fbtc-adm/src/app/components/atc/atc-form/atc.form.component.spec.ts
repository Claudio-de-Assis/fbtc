import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtcFormComponent } from './atc.form.component';

describe('AtcFormComponent', () => {
  let component: AtcFormComponent;
  let fixture: ComponentFixture<AtcFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtcFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtcFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
