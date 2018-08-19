import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociadoFormComponent } from './associado-self.form.component';

describe('AssociadoFormComponent', () => {
  let component: AssociadoFormComponent;
  let fixture: ComponentFixture<AssociadoFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssociadoFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssociadoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
