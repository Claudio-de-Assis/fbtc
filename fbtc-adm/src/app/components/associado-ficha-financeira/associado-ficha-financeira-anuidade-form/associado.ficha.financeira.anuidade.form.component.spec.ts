import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociadoFichaFinanceiraAnuidadeFormComponent } from './associado.ficha.financeira.anuidade.form.component';

describe('AssociadoFichaFinanceiraAnuidadeFormComponent', () => {
  let component: AssociadoFichaFinanceiraAnuidadeFormComponent;
  let fixture: ComponentFixture<AssociadoFichaFinanceiraAnuidadeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssociadoFichaFinanceiraAnuidadeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssociadoFichaFinanceiraAnuidadeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
