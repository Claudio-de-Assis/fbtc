import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociadoSelfFormComponent } from './associado-self.form.component';

describe('AssociadoSelfFormComponent', () => {
  let component: AssociadoSelfFormComponent;
  let fixture: ComponentFixture<AssociadoSelfFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssociadoSelfFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssociadoSelfFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
